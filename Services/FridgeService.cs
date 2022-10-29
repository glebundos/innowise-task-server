using innowise_task_server.Data;
using innowise_task_server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace innowise_task_server.Services
{
    public class FridgeService : ControllerBase, IFridgeService
    {
        private readonly ServerDbContext _context;
        public FridgeService(ServerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetFridges()
        {
            var fridges = await _context.Fridges
                .Include(f => f.Model)!
                .Include(f => f.Products)!
                .ThenInclude(p => p.Product)
                .ToListAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(fridges);
        }

        public async Task<IActionResult> GetFridgeById(Guid id)
        {
            var fridge = await _context.Fridges
                .Include(f => f.Products)!
                .ThenInclude(p => p.Product)
                .Include(f => f.Model)
                .FirstOrDefaultAsync(f => f.ID == id);
            return fridge == null ? NotFound() : Ok(fridge);
        }

        public async Task<IActionResult> GetFridgeProductById(Guid id)
        {
            var fridgeProduct = await _context.FridgeProducts
                .Include(fp => fp.Product)
                .Include(fp => fp.Fridge)
                .FirstOrDefaultAsync(f => f.ID == id);
            return fridgeProduct == null ? NotFound() : Ok(fridgeProduct);
        }

        public async Task<IActionResult> GetFridgeProductsById(Guid id)
        {
            var fridgeIncludingProducts = await _context.Fridges
                .Include(f => f.Products)!
                .ThenInclude(fp => fp.Product)
                .FirstAsync(f => f.ID == id);
            var products = fridgeIncludingProducts.Products;

            return products == null ? NotFound() : Ok(products);
        }

        public async Task<IActionResult> AddProduct(Product product, Guid fridgeId)
        {
            var fProduct = new FridgeProduct();
            fProduct.ProductID = product.ID;
            fProduct.FridgeID = fridgeId;
            fProduct.Quantity = (int)product.DefaultQuantity!;
            await _context.FridgeProducts.AddAsync(fProduct);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AddProduct), new { id = fProduct.ID }, fProduct);
        }

        public async Task<IActionResult> AddFridge(Fridge fridge)
        {
            await _context.Fridges.AddAsync(fridge);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AddFridge), new { id = fridge.ID }, fridge);
        }

        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var fProductToDelete = await _context.FridgeProducts.FindAsync(id);
            if (fProductToDelete == null) return NotFound();

            _context.FridgeProducts.Remove(fProductToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        public async Task<IActionResult> DeleteFridge(Guid id)
        {
            var fridgeToDelete = _context.Fridges.FindAsync(id).Result;
            if (fridgeToDelete == null) return NotFound();

            _context.Fridges.Remove(fridgeToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        public async Task<IActionResult> Update(Fridge fridge)
        {
            _context.SetModified(fridge);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        public async Task<IActionResult> AddFPQuantity_0()
        {
            string storedProc = "EXEC GetFPWhereQuantity_0";
            var fProducts = await _context.FridgeProducts.FromSqlRaw(storedProc).ToListAsync();
            foreach (var row in fProducts)
            {
                var product = _context.Products.FirstOrDefaultAsync(p => p.ID == row.ProductID).Result!;
                if (product.DefaultQuantity == null) continue;
                await DeleteProduct(row.ID);
                await AddProduct(product, row.FridgeID);
            }

            return Ok(fProducts);
        }

        public async Task<IActionResult> GetModels()
        {
            var models = await _context.FridgeModels
                .ToListAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(models);
        }

        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products
                .ToListAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(products);
        }

        public async Task<IActionResult> AddFridgeProducts(List<FridgeProduct> fridgeProducts)
        {
            for (int i = 0; i < fridgeProducts.Count(); i++)
            {
                if (_context.FridgeProducts.Any(p => p.ID == fridgeProducts[i].ID))
                {
                    _context.SetModified(fridgeProducts[i]);
                }
                else
                {
                    await _context.FridgeProducts.AddAsync(fridgeProducts[i]);
                }
            }

            await _context.SaveChangesAsync();
            return Ok(fridgeProducts);
        }
    }
}

using innowise_task_server.Data;
using innowise_task_server.Models;
using innowise_task_server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace innowise_task_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeController : ControllerBase
    {
        private readonly IFridgeService _fridgeService;

        public FridgeController(IFridgeService fridgeService)
        {
            _fridgeService = fridgeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Fridge>), StatusCodes.Status200OK)]
        public Task<IActionResult> Get()
        {
            return _fridgeService.GetFridges();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Fridge), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await _fridgeService.GetFridgeById(id);
        }

        [HttpGet("products/{id}")]
        [ProducesResponseType(typeof(FridgeProduct), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            return await _fridgeService.GetFridgeProductById(id);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetFridgeProductsById(Guid id)
        {
            return await _fridgeService.GetFridgeProductsById(id);
        }

        [HttpPost("product")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddProduct(Product product, Guid fridgeId)
        {
            return await _fridgeService.AddProduct(product, fridgeId);
        }

        [HttpPost("products")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddFridgeProducts(List<FridgeProduct> fridgeProducts)
        {
            return await _fridgeService.AddFridgeProducts(fridgeProducts);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddFridge(Fridge fridge)
        {
            return await _fridgeService.AddFridge(fridge);
        }

        [HttpDelete("products/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            return await _fridgeService.DeleteProduct(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFridge(Guid id)
        {
            return await _fridgeService.DeleteFridge(id);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Fridge fridge)
        {
            return await _fridgeService.Update(fridge);
        }

        [HttpGet("fill")]
        [ProducesResponseType(typeof(List<FridgeProduct>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddFPQuantity_0()
        {
            return await _fridgeService.AddFPQuantity_0();
        }

        [HttpGet("models")]
        [ProducesResponseType(typeof(IEnumerable<FridgeModel>), StatusCodes.Status200OK)]
        public Task<IActionResult> GetModels()
        {
            return _fridgeService.GetModels();
        }

        [HttpGet("products")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public Task<IActionResult> GetProducts()
        {
            return _fridgeService.GetProducts();
        }
    }
}

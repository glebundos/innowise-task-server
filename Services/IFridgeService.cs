using innowise_task_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace innowise_task_server.Services
{
    public interface IFridgeService
    {
        public Task<IActionResult> GetFridges();

        public Task<IActionResult> GetFridgeById(Guid id);

        public Task<IActionResult> GetFridgeProductById(Guid id);

        public Task<IActionResult> GetFridgeProductsById(Guid id);

        public Task<IActionResult> AddProduct(Product product, Guid fridgeId);

        public Task<IActionResult> AddFridgeProducts(List<FridgeProduct> fridgeProduct);

        public Task<IActionResult> AddFridge(Fridge fridge);

        public Task<IActionResult> DeleteProduct(Guid id);

        public Task<IActionResult> DeleteFridge(Guid id);

        public Task<IActionResult> Update(Fridge fridge);

        public Task<IActionResult> AddFPQuantity_0();

        public Task<IActionResult> GetModels();

        public Task<IActionResult> GetProducts();
    }
}
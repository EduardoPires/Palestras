using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Catalog.Services;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;
using WebMvc.Services;

namespace WebMvc.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly Catalog.CatalogClient _catalogClient;
        
        public CatalogController(
            ICatalogService catalogService, 
            Catalog.CatalogClient catalogClient)
        {
            _catalogService = catalogService;
            _catalogClient = catalogClient;
        }

        [HttpGet]
        [Route("catalog")]
        public async Task<IActionResult> Index()
        {
            //return View(await _catalogService.GetAll());

            var products = await _catalogClient.GetProductsAsync(new CatalogRequest());
            return View(MapToProduct(products));

        }

        private static List<ProductViewModel> MapToProduct(CatalogResponse catalogResponse)
        {
            var products = new List<ProductViewModel>();

            foreach (var item in catalogResponse.Items)
            {
                products.Add(new ProductViewModel
                {
                    Name = item.Name,
                    Price = (decimal)item.Price,
                    Image = item.Image,
                    Description = item.Description
                });
            }

            return products;
        }
    }
}

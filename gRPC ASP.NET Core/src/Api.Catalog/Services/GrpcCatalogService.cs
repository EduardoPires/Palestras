using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Catalog.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Api.Catalog.Services
{
    [Authorize]
    public class GrpcCatalogService : Catalog.CatalogBase
    {
        private readonly ILogger<GrpcCatalogService> _logger;
        public GrpcCatalogService(ILogger<GrpcCatalogService> logger)
        {
            _logger = logger;
        }

        public override Task<CatalogResponse> GetProducts(CatalogRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Calling Catalog");

            var products = new List<Product>();

            for (var i = 1; i < 5; i++)
            {
                products.Add(new Product
                {
                    Name = $"Product {i}",
                    Description = "Nice description",
                    Image = $"product{i}.jpg",
                    Price = 50
                });
            }

            _logger.LogInformation("Called Catalog");

            return Task.FromResult(MapToCatalogResponse(products));
        }

        private static CatalogResponse MapToCatalogResponse(IEnumerable<Product> products)
        {
            var response = new CatalogResponse();

            foreach (var product in products)
            {
                response.Items.Add(new CatalogItemResponse
                {
                    Name = product.Name,
                    Price = (double)product.Price,
                    Image = product.Image,
                    Description = product.Description
                });
            }

            return response;
        }
    }
}
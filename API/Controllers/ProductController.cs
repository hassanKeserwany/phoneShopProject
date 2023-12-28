using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
             _productRepository =  productRepository;
        }

        [HttpGet("Products")]
        public async Task<ActionResult<IReadOnlyList<Product>>>  getAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> getProductById(int id)
        {
            var products = await _productRepository.GetProductByIdAsync(id);
            return products;
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> getAllProductsBrands()
        {
            var Brands = await _productRepository.GetAllBrandsAsync();
            return Ok(Brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<List<ProductType>>> getAllProductsTypes()
        {
            var Types = await _productRepository.GetAllTypesAsync();
            return Ok(Types);
        }
    }
}

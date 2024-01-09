using API.Dtos;
using API.Helper;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
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
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo, 
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo ,IMapper mapper)
        {
             _productRepo =  productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        [HttpGet("Products")]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>>  getAllProducts([FromQuery] ProductSpecParams productParams)
            
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var totalItems = await _productRepo.CountAsync(countSpec);
            var products = await _productRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
                productParams.PageSize, totalItems, data));

        }
         
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> getProductById(int id)
        {
            var spec=new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);

            

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> getAllProductsBrands()
        {
            var Brands = await _productBrandRepo.ListAllAsync();
            return Ok(Brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<List<ProductType>>> getAllProductsTypes()
        {
            var Types = await _productTypeRepo.ListAllAsync();
            return Ok(Types);
        }
    }
}

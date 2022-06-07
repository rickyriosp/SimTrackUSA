using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _products;
        private readonly IGenericRepository<ProductBrand> _productBrands;
        private readonly IGenericRepository<ProductType> _productTypes;

        public ProductsController(IGenericRepository<Product> products, IGenericRepository<ProductBrand> productBrands, IGenericRepository<ProductType> productTypes)
        {
            _products = products;
            _productBrands = productBrands;
            _productTypes = productTypes;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var products = await _products.ListAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            Product product = await _products.GetByIdAsync(id);
            return product;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productBrands.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("brands/{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        {
            ProductBrand brand = await _productBrands.GetByIdAsync(id);
            return brand;
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var types = await _productTypes.ListAllAsync();
            return Ok(types);
        }

        [HttpGet("types/{id}")]
        public async Task<ActionResult<ProductType>> GetProductType(int id)
        {
            ProductType type = await _productTypes.GetByIdAsync(id);
            return type;
        }
    }
}
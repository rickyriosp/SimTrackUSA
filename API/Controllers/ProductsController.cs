using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _products;
        private readonly IGenericRepository<ProductBrand> _productBrands;
        private readonly IGenericRepository<ProductType> _productTypes;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> products, IGenericRepository<ProductBrand> productBrands, IGenericRepository<ProductType> productTypes, IMapper mapper)
        {
            _products = products;
            _productBrands = productBrands;
            _productTypes = productTypes;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string sort, int? brandId, int? typeId)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(sort, brandId, typeId);

            IReadOnlyList<Product> products = await _products.ListAsync(spec);
            IReadOnlyList<ProductToReturnDto> dtos = _mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            Product product = await _products.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(NotFound().StatusCode));

            ProductToReturnDto dto = _mapper.Map<Product, ProductToReturnDto>(product);

            return dto;
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
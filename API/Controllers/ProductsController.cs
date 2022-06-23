using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<ProductBrand> _productBrands;
    private readonly IGenericRepository<Product> _products;
    private readonly IGenericRepository<ProductType> _productTypes;

    public ProductsController(IGenericRepository<Product> products,
        IGenericRepository<ProductBrand> productBrands,
        IGenericRepository<ProductType> productTypes,
        IMapper mapper)
    {
        _products = products;
        _productBrands = productBrands;
        _productTypes = productTypes;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
        [FromQuery] ProductSpecParams productParams)
    {
        var countSpec = new ProductWithFiltersForCountSpecification(productParams);
        var totalItems = await _products.CountAsync(countSpec);

        var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
        var products = await _products.ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

        return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems,
            data));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);

        var product = await _products.GetEntityWithSpec(spec);

        if (product == null) return NotFound(new ApiResponse(NotFound().StatusCode));

        var data = _mapper.Map<Product, ProductToReturnDto>(product);

        return data;
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
        var brand = await _productBrands.GetByIdAsync(id);
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
        var type = await _productTypes.GetByIdAsync(id);
        return type;
    }
}

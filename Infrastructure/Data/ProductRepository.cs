using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.Id == id);
        return product;
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .AsSplitQuery()
            .ToListAsync();
        return products;
    }

    public async Task<ProductBrand> GetProductBrandByIdAsync(int id)
    {
        var brand = await _context.ProductBrands.FindAsync(id);
        return brand;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        var brands = await _context.ProductBrands.ToListAsync();
        return brands;
    }

    public async Task<ProductType> GetProductTypeByIdAsync(int id)
    {
        var type = await _context.ProductTypes.FindAsync(id);
        return type;
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        var types = await _context.ProductTypes.ToListAsync();
        return types;
    }
}

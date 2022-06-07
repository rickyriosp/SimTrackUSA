using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            List<Product> products = await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .AsSplitQuery()
                .ToListAsync();
            return products;
        }

        public async Task<ProductBrand> GetProductBrandByIdAsync(int id)
        {
            ProductBrand brand = await _context.ProductBrands.FindAsync(id);
            return brand;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            List<ProductBrand> brands = await _context.ProductBrands.ToListAsync();
            return brands;
        }

        public async Task<ProductType> GetProductTypeByIdAsync(int id)
        {
            ProductType type = await _context.ProductTypes.FindAsync(id);
            return type;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            List<ProductType> types = await _context.ProductTypes.ToListAsync();
            return types;
        }
    }
}
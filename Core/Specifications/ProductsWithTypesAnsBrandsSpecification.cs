using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(string sort, int? brandId, int? typeId)
            : base(p => (!brandId.HasValue || p.ProductBrandId == brandId) &&
                        (!typeId.HasValue || p.ProductTypeId == typeId))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(p => p.Name);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            // ThenInclude Example
            // AddInclude(“CountyOfOrigin.DefaultCountryLanguage”);
        }
    }
}
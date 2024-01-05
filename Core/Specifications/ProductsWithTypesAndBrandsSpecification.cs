using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecifications<Product>
    {

        public ProductsWithTypesAndBrandsSpecification(string? sort , int? brand_id, int? type_id):
            base(x=>
            (!brand_id.HasValue || x.ProductBrandId==brand_id )&& 
            (!type_id.HasValue || x.ProductTypeId==type_id ) )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);

            if(sort != null)
            {
                switch(sort)
                {
                    case "priceAsc":
                        AddOrderBy(p=>p.Price); break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price); break;
                        default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) :
            base(x => x.Id == id) // Add the condition to check if x.Id matches the provided id
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}

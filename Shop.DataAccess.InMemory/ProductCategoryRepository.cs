using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        //Objet complexe on initialise dans le constructeur
        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        //Commit équivalent du SaveChanges() de EF (context)
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory pc)
        {
            productCategories.Add(pc);
        }

        public void Update(ProductCategory productC)
        {
            ProductCategory pCToUpdate = productCategories.Find(p => p.Id == productC.Id);
            if (pCToUpdate != null)
            {
                pCToUpdate = productC;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public ProductCategory FindById(int id)
        {
            ProductCategory p = productCategories.Find(p1 => p1.Id == id);
            if (p != null)
            {
                return p;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(int id)
        {
            ProductCategory pCToDelete = productCategories.Find(p => p.Id == id);
            if (pCToDelete != null)
            {
                productCategories.Remove(pCToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}

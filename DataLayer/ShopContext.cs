
using DomainClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ShopContext : DbContext
    {
        public ShopContext() : base("ShopContext")
        {
            Database.SetInitializer<ShopContext>(new ShopDBinitializer());
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderedProduct> OrderedProducts { get; set; }



    }




    public class ShopDBinitializer : CreateDatabaseIfNotExists<ShopContext>
    {
        protected override void Seed(ShopContext context)
        {
            ICollection<ProductType> defaultStandards = new List<ProductType>();

            ICollection<ProductType> initProdCategoryes = ProductType.GetProductTypes();
            foreach (ProductType cat in initProdCategoryes)
                context.ProductTypes.Add(cat);

            base.Seed(context);
        }
    }

}



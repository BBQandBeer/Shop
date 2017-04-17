using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DomainClasses
{




    public class ProductType

    {
        public ProductType()
        {
            this.Products = new HashSet<Product>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }


        public static ICollection<ProductType> GetProductTypes()  // method to fill initial data when CreateIfEmpty
        {
            IList<ProductType> myTypes = new List<ProductType>();
            myTypes.Add(new ProductType { ProductTypeId = 1, ProductTypeName = "Топли напитки", Description = "" });
            myTypes.Add(new ProductType { ProductTypeId = 2, ProductTypeName = "Алкохолни напитки", Description = "" });
            myTypes.Add(new ProductType { ProductTypeId = 3, ProductTypeName = "Газирани напитки", Description = "" });
            myTypes.Add(new ProductType { ProductTypeId = 4, ProductTypeName = "Хранителни продукти", Description = "" });
            myTypes.Add(new ProductType { ProductTypeId = 4, ProductTypeName = "Цигари", Description = "" });


            return myTypes;

        }





    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace DomainClasses
{

    public class Product

    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] Picture { get; set; }

        public virtual IList<OrderedProduct> OrderedProducts { get; set; }
        public ProductType ProductType { get; set; }
        //public Order OrderId { get; set; }
        //[ForeignKey("SalesId")]
        //public Sales Sales { get; set; }




    }
}

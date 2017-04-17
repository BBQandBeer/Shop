using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
    public class Order
    {
        public Order()
        {
            this.OrderedProducts = new HashSet<OrderedProduct>();
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public DateTime OrderDateTime { get; set; }

        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }



        //public virtual ObservableCollection<Product> ProductsOrdered { get; set; }

    }
}

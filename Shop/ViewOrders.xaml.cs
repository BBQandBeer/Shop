using DataLayer;
using DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shop
{
    /// <summary>
    /// Interaction logic for VewOrders.xaml
    /// </summary>
    public partial class ViewOrders : Window
    {
        private ShopContext vOrder = new ShopContext();

        public ViewOrders()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();
            List<DataSourceItem> dataSource = new List<DataSourceItem>();
            List<Order> orderList = vOrder.Orders.ToList();
            foreach (var ord in orderList)
            {
                DataSourceItem newItem = new DataSourceItem();
                newItem.OrderId = ord.OrderId;
                newItem.OrderDateTime = ord.OrderDateTime;
                newItem.OrderedProducts = "";
                newItem.TotalPrice = 0m;
                List<OrderedProduct> orderedProducts = vOrder.OrderedProducts
                                                             .Where(o => o.OrderID == ord.OrderId)
                                                             .ToList();
                foreach (var op in orderedProducts)
                {
                    Product prod = vOrder.Products
                                         .Where(p => p.ProductId == op.ProductID)
                                         .SingleOrDefault();
                    newItem.OrderedProducts += prod.Description + " - " + op.Quantity + "x" + prod.Price + "лв." + ";\n ";
                    newItem.TotalPrice += (op.Quantity * prod.Price);
                }
                dataSource.Add(newItem);
            }
            dataGrid.ItemsSource = dataSource;
        }
        public class DataSourceItem
        {
            public int OrderId { get; set; }
            public DateTime OrderDateTime { get; set; }
            public string OrderedProducts { get; set; }
            public decimal TotalPrice { get; set; }
        }
    }
}

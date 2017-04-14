using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
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
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using DataLayer;
using DomainClasses;

namespace Shop
{
    /// <summary>
    /// Interaction logic for ViewProducts.xaml
    /// </summary>
    public partial class ViewProducts : Window
    {
        private ShopContext prod = new ShopContext();




        public ViewProducts()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();


            dataGrid.ItemsSource = prod.Products.ToList();
            filterBox.ItemsSource = prod.ProductTypes.Select(x => x.ProductTypeName).ToList();
            // filterBox.SelectedValue= prod.ProductTypes.ToList().ToString();//.Select(x => x.ProductTypeId).ToList().ToString();

        }

        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var newContext = ((IObjectContextAdapter)prod).ObjectContext;

            //ObjectQuery<Product> filterProd = new ObjectQuery<Product>(
            //   "Select VALUE pr from Products AS pr Where pr.ProductTypeId =" + (filterBox.SelectedIndex + 1), newContext);

            var filterProd = prod.Products.Where(x=>x.ProductTypeId==(filterBox.SelectedIndex+1)).ToList();

            dataGrid.ItemsSource = filterProd;

        }
    }
}

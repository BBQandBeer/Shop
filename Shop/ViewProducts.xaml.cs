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
using System.Collections.ObjectModel;
using System.Reflection;

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

            var prodCollection = new ObservableCollection<Product>(prod.Products.ToList());



            dataGrid.ItemsSource = prodCollection;// prod.Products.ToList();
            filterBox.ItemsSource = prod.ProductTypes.Select(x => x.ProductTypeName).ToList();
            // filterBox.SelectedValue= prod.ProductTypes.ToList().ToString();//.Select(x => x.ProductTypeId).ToList().ToString();

        }



        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var newContext = ((IObjectContextAdapter)prod).ObjectContext;

            //ObjectQuery<Product> filterProd = new ObjectQuery<Product>(
            //   "Select VALUE pr from Products AS pr Where pr.ProductTypeId =" + (filterBox.SelectedIndex + 1), newContext);

            var filterProd = prod.Products.Where(x => x.ProductTypeId == (filterBox.SelectedIndex + 1)).ToList();

            dataGrid.ItemsSource = new ObservableCollection<Product>(filterProd.ToList());

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = new ObservableCollection<Product>(prod.Products.ToList());

        }

        private void Button_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int selectedProductId = (dataGrid.SelectedItem as Product).ProductId;
                Product pr = prod.Products.Where(produ => produ.ProductId == selectedProductId).SingleOrDefault();
                var prTypeDeleted = pr.ProductType;
                prod.Products.Remove(pr);
                prod.SaveChanges();
                var prodCollection = new ObservableCollection<Product>(prod.Products.ToList());

                var filterProd = prod.Products.Where(x => x.ProductTypeId == (filterBox.SelectedIndex + 1)).ToList();

                dataGrid.ItemsSource = new ObservableCollection<Product>(filterProd.ToList());

            }

            catch
            {

                MessageBox.Show("Моля маркирайте ред за да изтриете продукта");
                return;
            }



        }

        private void Button_Click_Edit(object sender, MouseButtonEventArgs e)
        {
            try
            {


                //MessageBox.Show("Моля, изберете клетка за да редактирате продукта и натиснете отново след редакция");
                prod.SaveChanges();
                //MessageBox.Show("Успешно редактирахте продукта");


            }

            catch
            {

                MessageBox.Show("Моля, изберете клетка за да редактирате продукта");
                return;
            }



        }

    }// end class view product
}


//var myObservableCollection = new ObservableCollection<YourType>(myIEnumerable);
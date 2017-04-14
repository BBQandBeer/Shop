using DataLayer;
using DomainClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shop
{
    /// <summary>
    /// Interaction logic for CoffeeShopPOS.xaml
    /// </summary>
    public partial class CoffeeShopPOS : Window
    {
        private BindingList<Product> products = new BindingList<Product>();
        private ShopContext shopContext = new ShopContext();

        private decimal purchase;

        public decimal Purchase
        {
            get { return purchase; }
            set
            {

                purchase = value;

                textBoxTotal.Text = purchase.ToString();

            }

        }

        public CoffeeShopPOS()
        {

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


            InitializeComponent();


            listProductsChosen.ItemsSource = products;

            createTabbetPannel();



        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Product p = new Product() { Description = "Product A", Price = 1.99m };

            products.Add(p);
        }


        private void createTabbetPannel()
        {


            var i = 1;

            foreach (ProductType pt in shopContext.ProductTypes.ToList())
            {


                TabItem tab = new TabItem();
                tab.Header = pt.ProductTypeName;
                tabControl1.Items.Add(tab); //CREATE TAB WITH PROD CAT. NAME

                WrapPanel myWrap = new WrapPanel();
                myWrap.Name = "myWrap";
                myWrap.HorizontalAlignment = HorizontalAlignment.Left;
                myWrap.VerticalAlignment = VerticalAlignment.Top;
                myWrap.Height = 514;
                myWrap.Width = 818;
                myWrap.Margin = new Thickness(60, 32, 0, 0);

                var filteredProduct = shopContext.Products.Where(p => p.ProductTypeId == i);

                foreach (var fp in filteredProduct.ToList())
                {

                    Button b = new Button() { Height = 100, Width = 100 };

                    b.Content = fp.Description;
                    var btnName = fp.Description.Replace(" ","_");
                    fp.Description = btnName;
                    b.Name = fp.Description.ToString();

                    b.Tag = fp;

                    b.Click += new RoutedEventHandler(UpdateProductList);

                    myWrap.Children.Add(b);

                }

                tab.Content = myWrap;

                i++;

            }


        }

        private void UpdateProductList(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            Product p = (Product)b.Tag;

            products.Add(p);

            UpdateCustomerInformationPannel(p);

            Purchase = Purchase + p.Price;

            listProductsChosen.SelectedIndex = listProductsChosen.Items.Count - 1;

        }


        private void UpdateCustomerInformationPannel(Product product)
        {
            string currentDescription = product.Description;
            string currentPrice = String.Format("{0:f2}", product.Price);
            string currentDescriptionPadded = currentDescription.PadRight(30);

            textInfoPanel.Text = currentDescriptionPadded + product.Price;

        }



        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = (Product)listProductsChosen.SelectedItem;
            Purchase = Purchase - selectedProduct.Price;

            products.Remove(selectedProduct);


        }

        private void OpenPayment(object sender, RoutedEventArgs e)
        {
            Payment payment = new Payment();
            payment.Show();
            payment.PaymentMade += Payment_Success;
            payment.PaymentAmount = purchase;
        }

        private void Payment_Success(object Sender, PaymentMadeEventArgs e)
        {

            Order order = new Order();
            order.OrderDateTime = DateTime.Now;

            if (e.PaymentSuccess == true)
            {
                //save transaction;
                foreach (var product in products)
                {
                    

                    shopContext.OrderedProducts.Add(new OrderedProduct { ProductID = product.ProductId });


                }

                shopContext.Orders.Add(order);
                shopContext.SaveChanges();

                MessageBox.Show(e.PaymentSuccess.ToString());
            }
        }
    }
}

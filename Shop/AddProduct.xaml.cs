using DataLayer;
using DomainClasses;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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



namespace Shop
{

    public partial class AddProduct : Window
    {
        ShopContext prod = new ShopContext();
        ProductType newProdTypes = new ProductType();

        IList<ProductType> productTypesToCreate = ProductType.GetProductTypes();



        private byte[] imageFile; // picture variable declaration
        public AddProduct()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            DataContext = ProductType.GetProductTypes().Select(x => x.ProductTypeName).ToList();



        }

        private void buttonUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FileStream imgFileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                using (imgFileStream)
                {
                    imageFile = new byte[imgFileStream.Length];
                    imgFileStream.Read(imageFile, 0, imageFile.Length);
                }


                MemoryStream msImage = new MemoryStream(imageFile);
                using (msImage)
                {
                    var imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = msImage;
                    imageSource.EndInit();

                    // Assign the Source property of your image
                    imageToAdd.Source = imageSource;


                    // imageToAdd.ImageFailed=

                    //FromStream(msImage);

                }



            }

        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

            //ProductType newProductType = new ProductType();
            //var products = ProductType.GetProductTypes();
            //newProductType.Add(products[1]);
            Product productExist = new ShopContext().Products.Where(p => p.Description == textBoxDeskr.Text).SingleOrDefault();
            if (productExist == null)
            { 
                Product newProduct = new Product();

                newProduct.Description = textBoxDeskr.Text;

                newProduct.Price = decimal.Parse(textBoxPrice.Text);

                newProduct.Picture = imageFile;

                newProduct.ProductTypeId = comboCategories.SelectedIndex + 1;

                prod.Products.Add(newProduct);

                prod.SaveChanges();

                MessageBox.Show("Продукта е записан");
            }
            Close();
            AddProduct addedProduct = new AddProduct();
            addedProduct.Show();
        }
    }
}

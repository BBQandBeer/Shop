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
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        public delegate void PaymentMadeEvent(object Sender, PaymentMadeEventArgs e);

        public event PaymentMadeEvent PaymentMade;

        public decimal paymentAmount;

        public decimal PaymentAmount
        {
            get { return paymentAmount; }
            set
            {
                paymentAmount = value;
                textBoxBill.Text = String.Format("{0:f2}", paymentAmount);
            }
        }

        public Payment()
        {
            InitializeComponent();
        }

        private void PaymentHasBeenMade(object sender, RoutedEventArgs e)
        {
            decimal total = 0;

            try
            {
                total = decimal.Parse(textBoxBill.Text) - decimal.Parse(textBoxPayed.Text);
            }

            catch
            {

                MessageBox.Show("Грешка при въвеждането. Моля въведете валидна сума!");
                return;
            }



            if (total > 0)
            {
                textBoxBill.Text = total.ToString();
            }

            else
            {
                MessageBox.Show("Сума за връщане " + String.Format("{0:f2}", -total));
                PaymentMade(this, new PaymentMadeEventArgs() { PaymentSuccess = true });
            }



        }
    }


    public class PaymentMadeEventArgs : EventArgs
    {
        private bool paymentSuccess;

        public bool PaymentSuccess
        {

            get { return paymentSuccess; }
            set { paymentSuccess = value; }

        }


    }
}




















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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RandomMachine randomMachine;
        public MainWindow()
        {
            InitializeComponent();
            input.Clear();
            output.Content = "";
            randomMachine = new RandomMachine();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(input.Text))
            {
                randomMachine.AddCoupon(input.Text);
                output.Content = "Dodano kupon: " + input.Text + "\nZawartość maszyny: \n";
                foreach (String coupon in randomMachine.GetAllCoupons())
                {
                    output.Content += coupon + " ";
                }
            }
            input.Clear();
            
        }

        private void outputButton_Click(object sender, RoutedEventArgs e)
        {
           
            if(randomMachine.CouponsAvailable())
            {
                string outCoupon = randomMachine.GetCoupon();
                output.Content = "Wyjęto kupon: " + outCoupon + "\nZawartość maszyny: \n";
                foreach (String coupon in randomMachine.GetAllCoupons())
                {
                    output.Content += coupon + " ";
                }
            }
            else
            {
                output.Content = "Nie wyjęto kuponu! \nZawartość maszyny: \n";
                foreach (String coupon in randomMachine.GetAllCoupons())
                {
                    output.Content += coupon;
                }
            }
        }
    }
}

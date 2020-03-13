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

namespace Zadanie1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Machine machine;
        public MainWindow()
        {
            machine = new Machine();
            InitializeComponent();
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            
            string newCoupon = newcoupon.Text;

            if (string.IsNullOrEmpty(newCoupon))
            {
                List<string> tempCouponsList = machine.getCoupons();
                newcoupon.Clear();
                machine.addCoupon(newCoupon);
                couponLabel.Content = "CouponAdded - dodano kupon - ";
                couponLabel.Content = couponLabel.Content + "Stan maszyny:{";
                foreach (var machineCoupon in tempCouponsList)
                {
                    couponLabel.Content = couponLabel.Content + machineCoupon+",";
                }
                couponLabel.Content = couponLabel.Content + "}";
            }
            else couponLabel.Content = "InputEmpty - nie można dodać"; 

        }

        private void get_Click(object sender, RoutedEventArgs e)
        {
            if (machine.couponsState() != false)
            {
                List<string> tempCouponsList = machine.getCoupons();
                couponLabel.Content = "ReceivedCoupon-zdjęto kupon: "+ machine.getCoupon();
                couponLabel.Content = couponLabel.Content + "Stan maszyny:{";
                foreach (var machineCoupon in tempCouponsList)
                {
                    couponLabel.Content = couponLabel.Content + machineCoupon + ",";
                }
                couponLabel.Content = couponLabel.Content + "}";
            }
            else couponLabel.Content = "MachineEmpty - brak kuponów";

        }
    }
}

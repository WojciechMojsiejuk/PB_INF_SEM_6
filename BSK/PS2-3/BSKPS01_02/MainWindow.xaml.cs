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

namespace BSKPS01_02
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Zadanie1();
        }

        private void Zadanie1_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Zadanie1();
        }

        private void Zadanie2_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Zadanie2();
        }

        private void Zadanie31_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Zadanie3_1();
        }

        private void Zadanie32_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Zadanie3_2();
        }

        private void Zadanie4_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Zadanie4();
        }

        private void Zadanie5_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Zadanie5();
        }
    }
}

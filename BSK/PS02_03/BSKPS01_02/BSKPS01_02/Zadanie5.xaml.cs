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
    /// Logika interakcji dla klasy Zadanie5.xaml
    /// </summary>
    public partial class Zadanie5 : Page
    {
        public Zadanie5()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            output.Text = Zadanie5_WM.Cypher(messageInput.Text, keyInput.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            output.Text = Zadanie5_WM.Decypher(messageInput.Text, keyInput.Text);
        }
    }
}

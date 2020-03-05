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
    /// Logika interakcji dla klasy Zadanie4.xaml
    /// </summary>
    public partial class Zadanie4 : Page
    {
        public Zadanie4()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            output.Text = Zadanie4_IS.Cypher(messageInput.Text, nInput.Text, k0Input.Text, k1Input.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            output.Text = Zadanie4_IS.Decypher(messageInput.Text, nInput.Text, k0Input.Text, k1Input.Text);
        }
    }
}

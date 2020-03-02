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
    /// Logika interakcji dla klasy Zadanie3_2.xaml
    /// </summary>
    public partial class Zadanie3_2 : Page
    {
        public Zadanie3_2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            output.Text = Zadanie3_2_WM.Cypher(messageInput.Text, keyInput.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            output.Text = Zadanie3_2_WM.Decypher(messageInput.Text, keyInput.Text);
        }
    }
}

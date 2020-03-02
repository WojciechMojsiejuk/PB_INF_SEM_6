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
    /// Logika interakcji dla klasy Zadanie3_1.xaml
    /// </summary>
    public partial class Zadanie3_1 : Page
    {
        public Zadanie3_1()
        {
            InitializeComponent();
        }

        private void CipherButton_Click(object sender, RoutedEventArgs e)
        {
            output.Text = Zadanie3_1_WM.Cypher(messageInput.Text, keyInput.Text);
        }

        private void DecipherButton_Click(object sender, RoutedEventArgs e)
        {
            output.Text = Zadanie3_1_WM.Decypher(messageInput.Text, keyInput.Text);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace zadanie5
{
    /// <summary>
    /// Logika interakcji dla klasy NonModal.xaml
    /// </summary>
    public partial class NonModal : Window
    {
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string email { get; set; }
        public NonModal()
        {
            InitializeComponent();
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {          
            Imie.SelectAll();
            Imie.Focus();
        }

        private void imie_changed(object sender, TextChangedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).ProcessImie(Imie.Text);
        }

        private void nazwisko_changed(object sender, TextChangedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).ProcessNazwisko(Nazwisko.Text);
        }

        private void email_changed(object sender, TextChangedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).ProcessEmail(Email.Text);
        }
    }
}

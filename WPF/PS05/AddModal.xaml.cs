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
    /// Logika interakcji dla klasy AddModal.xaml
    /// </summary>
    public partial class AddModal : Window
    {
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string email { get; set; }
        public AddModal()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Imie.Text = imie;
            Nazwisko.Text = nazwisko;
            Email.Text = email;
            Imie.SelectAll();
            Imie.Focus();
        }

        private void OnOK(object sender, RoutedEventArgs e)
        {
            if(Imie.Text.Length == 0)
            {
                MessageBox.Show("Uzupełnij puste pola elementu.", "Edycja",
                  MessageBoxButton.OK, MessageBoxImage.Warning);
                Imie.Focus();
            }
            else if(Nazwisko.Text.Length == 0)
            {
                MessageBox.Show("Uzupełnij puste pola elementu.", "Edycja",
                  MessageBoxButton.OK, MessageBoxImage.Warning);
                Nazwisko.Focus();
            }
            else if(Email.Text.Length == 0)
            {
                MessageBox.Show("Uzupełnij puste pola elementu.", "Edycja",
                  MessageBoxButton.OK, MessageBoxImage.Warning);
                Email.Focus();
            }
            else {
                imie = Imie.Text;
                nazwisko = Nazwisko.Text;
                email = Email.Text;
                DialogResult = true;
            }
            
        }
    }
}

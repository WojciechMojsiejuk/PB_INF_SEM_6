using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Zadanie_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Collection<Osoba> Osoby { get; } = new ObservableCollection<Osoba>();
        public MainWindow()
        {
            InitializeComponent();
            Osoby.Add(new Osoba("xd", "xdd"));
            Osoby.Add(new Osoba("lol", "lolol"));
        }
        
        private void checked_dane(object sender, RoutedEventArgs e)
        {
            Email.IsEnabled = true;
            Kwota.IsEnabled = true;
            Area.IsEnabled = true;
            Level.IsEnabled = true;
        }

        private void unchecked_dane(object sender, RoutedEventArgs e)
        {
            Email.IsEnabled = false;
            Kwota.IsEnabled = false;
            Area.IsEnabled = false;
            Level.IsEnabled = false;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Osoby.Add(new Osoba());
            Lista.SelectedIndex = Osoby.Count - 1;
            boxImie.Text = "imie";
            boxImie.SelectAll();
            boxImie.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lista.ItemsSource = Osoby;
            Area.Items.Add("Białystok");
            Area.Items.Add("Warszawa");
            Area.Items.Add("Poznań");
            Area.Items.Add("Kraków");
         
        }

        private void Usuń_Click(object sender, RoutedEventArgs e)
        {
            if (Lista.SelectedIndex >= 0)
                Osoby.RemoveAt(Lista.SelectedIndex);
        }
    }
}

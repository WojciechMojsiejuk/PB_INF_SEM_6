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

namespace zadanie5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User CurrentUser;
        NonModal window;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void zamknijOkno(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            var dlg = new AddModal();
            dlg.Owner = this;
            if (true == dlg.ShowDialog())
            {
                lista.Items.Add(new User { Imie = dlg.imie, Nazwisko = dlg.nazwisko, Email = dlg.email });
            }
                
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if (lista.SelectedIndex >= 0)
            {
                if (MessageBox.Show(
                        "Czy na pewno chcesz usunąć wskazany element?",
                        "Usuń element", MessageBoxButton.YesNo,
                        MessageBoxImage.Question) != MessageBoxResult.Yes)
                    return;
                lista.Items.RemoveAt(lista.SelectedIndex);
            }
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            if (lista.SelectedIndex >= 0)
            {
                var dlg = new AddModal();
                dlg.Owner = this;
                var it = lista.SelectedItem as User;
                dlg.imie = it.Imie;
                dlg.nazwisko = it.Nazwisko;
                dlg.email = it.Email;
                if (true == dlg.ShowDialog())
                {
                    it.Email = dlg.email;
                    it.Imie = dlg.imie;
                    it.Nazwisko = dlg.nazwisko;
                    lista.Items.Refresh();
                }
            }
        }
        public void ProcessImie(string text)
        {
            CurrentUser.Imie = text;
            lista.Items.Refresh();
        }
        public void ProcessNazwisko(string text)
        {
            CurrentUser.Nazwisko = text;
            lista.Items.Refresh();
        }
        public void ProcessEmail(string text)
        {
            CurrentUser.Email = text;
            lista.Items.Refresh();
        }
        private void PreviewUser(object sender, RoutedEventArgs e)
        {
            if (lista.SelectedItem == null)
                return;
            window = new NonModal();
            window.Title = "Edycja RealTime";           
            window.Imie.Text = CurrentUser.Imie;
            window.Nazwisko.Text = CurrentUser.Nazwisko;
            window.Email.Text = CurrentUser.Email;
            window.Owner = this;
            window.Show();
        }

        private void select_user(object sender, SelectionChangedEventArgs e)
        {
            CurrentUser = lista.SelectedItem as User;
            if(window!=null && CurrentUser!=null)
            {
                window.Imie.Text = CurrentUser.Imie;
                window.Nazwisko.Text = CurrentUser.Nazwisko;
                window.Email.Text = CurrentUser.Email;
            }
        }
    }
}

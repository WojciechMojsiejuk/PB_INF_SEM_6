using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Zadanie3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double cena;
        int nakład;
        double jednostkowa;
        double wydruk;
        string wielkosc;
        int przesyłka;
        double kolorowy;
        double rabat;
        double gramatura;
        string gram;
        StringBuilder podsumowanie;
        public MainWindow()
        {         
            InitializeComponent();
            cena = 100;
            gramatura = 1;
            jednostkowa = 0.2;
            nakład = 0;
            wielkosc = "A0 - cena 20gr/szt.";
            rabat = 0;
            kolorowy = 0;
            wydruk = 0;
            przesyłka = 0;
            podsumowanie = new StringBuilder(300);
            wyliczPodsumowanie();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            
        }
        private void wyliczPodsumowanie()
        {
            double afterDiscount = 0;
            string tmp = PaperLabel.Content.ToString();
            cena = nakład * jednostkowa * gramatura ;
            cena = cena + cena * kolorowy;
            cena = cena + cena * wydruk;
            cena = cena + przesyłka;
            if (nakład >= 100 && nakład < 900)
            {
                rabat = Math.Round((double)nakład / 10000, 2);
            }
            else if (nakład >= 900 && nakład < 1000) rabat = 0.09;
            else if (nakład >= 1000) rabat = 0.1;
            else rabat = 0;
            afterDiscount = Math.Round(cena - cena * rabat,2);          
            Description.Text = Math.Round(cena,2) + " rabat " + rabat + " porabacie "+ Math.Round(afterDiscount, 2);
            podsumowanie.Clear();
            podsumowanie.AppendLine("Przedmiot Zamówienia: " + nakład + " szt., format " + tmp[0] + tmp[1] + ", "+gram.Substring(0,8)+", druk");
            podsumowanie.AppendLine("Cena przed rabatem: " + cena+"zł");
            podsumowanie.AppendLine("Naliczony rabat: " + rabat * 100 + "%");
            podsumowanie.AppendLine("Cena po rabacie: " + afterDiscount + "zł");
            Description.Text = podsumowanie.ToString();

        }
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Nakład.Text = "0";
            PaperFormat.Value = 0;
            PaperLabel.Content = "A0 - cena 20gr/szt.";
            Kolorowy.IsChecked = false;
            initial.IsChecked = true;
            foreach(CheckBox check in opcje.Children)
            {
                check.IsChecked = false;
            }
            standard.IsChecked = true;
            MessageBox.Show("Wysłano Zamówienie");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EnableKolorowy(object sender, RoutedEventArgs e)
        {
            kolorowy = 0.5;
            KolorowyCombo.IsEnabled = true;
            wyliczPodsumowanie();
        }
        private void DisableKolorowy(object sender, RoutedEventArgs e)
        {
            kolorowy = 0;
            KolorowyCombo.IsEnabled = false;
            wyliczPodsumowanie();
        }

        private void PaperFormatChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = 20;
          for(int i=0;i<e.NewValue;i++)
            {
                value = Math.Round(value * 2.5,0);
            }
                PaperLabel.Content = "A"+e.NewValue +" - cena "+value+"gr/szt.";
            jednostkowa = value/100;
            wyliczPodsumowanie();
        }

        private void Radio_checked(object sender, RoutedEventArgs e)
        {
            gramatura = Convert.ToDouble(((RadioButton)sender).Tag);
            gram = (((RadioButton)sender).Content).ToString();
            if (Description != null)wyliczPodsumowanie();         
        }
       

        private void count_pages(object sender, TextChangedEventArgs e)
        {
            string text = Nakład.Text;
            if (!string.IsNullOrEmpty(text))nakład = int.Parse(text);           
            if(Description!=null)wyliczPodsumowanie();
        }

        private void Wydruk_checked(object sender, RoutedEventArgs e)
        {
            wydruk = wydruk + Convert.ToDouble(((CheckBox)sender).Tag);
            wyliczPodsumowanie();
        }
        private void Wydruk_unchecked(object sender, RoutedEventArgs e)
        {
            wydruk = wydruk - Convert.ToDouble(((CheckBox)sender).Tag);
            wyliczPodsumowanie();
        }

        private void Delivery_Checked(object sender, RoutedEventArgs e)
        {
            przesyłka = Convert.ToInt16(((RadioButton)sender).Tag);
            if (Description != null) wyliczPodsumowanie();
        }
    }
}

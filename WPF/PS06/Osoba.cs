using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Zadanie_6
{
    public class Osoba : INotifyPropertyChanged
    {
        public Osoba() { }
        public Osoba(string imie,string nazwisko) 
        { 
            Imie = imie;
            Nazwisko = nazwisko;
        }
        public Osoba(string imie, string nazwisko,string email,double kwota,string region) {
            Imie = imie;
            Nazwisko = nazwisko;
            Email = email;
            Kwota = kwota;

        }
        private string imie;
        public string Imie
        {
            get { return imie; }
            set { imie = value;OnPropertyChanged("ImieNazwisko"); }
        }
        private string nazwisko;
        public string Nazwisko
        {
            get { return nazwisko; }
            set { nazwisko = value; OnPropertyChanged("ImieNazwisko"); }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("ImieNazwisko"); }
        }
        private double kwota;
        public double Kwota
        {
            get { return kwota; }
            set { kwota = value; OnPropertyChanged("ImieNazwisko"); }
        }
        private double poziom;
        public double Poziom
        {
            get { return poziom; }
            set { poziom = value; OnPropertyChanged("ImieNazwisko"); }
        }
        private string region;
        public string Region
        {
            get { return region; }
            set { region = value; OnPropertyChanged("ImieNazwisko"); }
        }
        public string ImieNazwisko
        {
            get
            {
                return Imie + " " + Nazwisko;
            }
        }
        public override string ToString()
        {
            return Imie + " " + Nazwisko;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}

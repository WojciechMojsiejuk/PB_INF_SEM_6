using System;
using System.Collections.Generic;
using System.Text;

namespace zadanie5
{
    public class User
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set;  }
        public override string ToString()
        {
            return Imie + ", " + Nazwisko + ", " + Email;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary
{
    public class PersonModel
    {
        public string Pesel { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public PersonModel(string pesel, string nazwisko, string imie)
        {
            Pesel = pesel;
            Nazwisko = nazwisko;
            Imie = imie;
        }

        public override string ToString()
        {
            return String.Format("Pesel: {0, -20} Nazwisko: {1, -24} Imie: {2}", Pesel, Nazwisko, Imie);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_projekat.Models
{
    public enum Polovi {Zensko,Musko }
    public enum Uloge {Turista, Administrator, Menadzer }

    public class Model_Korisnik
    {
        public string Korisnicko_Ime { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Polovi Pol { get; set; }
        public string Email { get; set; }
        public string Datum_Rodjenja { get; set; }
        public Uloge Uloga { get; set; }
        public List<string> Aranzmani { get; set; }
        public bool Obrisan { get; set; }
        public int Otkazivanja { get; set; }

        public Model_Korisnik() { }

        public Model_Korisnik(string korisnicko_ime, string lozinka, string ime, string prezime, string pol, string email, string datum, string uloga, bool obrisan, List<string> aranzmani, int otkazivanja)
        {
            Korisnicko_Ime = korisnicko_ime;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Aranzmani = aranzmani;
            Email = email;
            Datum_Rodjenja = datum;
            Obrisan = obrisan;
            Otkazivanja = otkazivanja;
            switch (uloga)
            {
                case "Turista":
                    Uloga = Uloge.Turista;
                    break;
                case "Administrator":
                    Uloga = Uloge.Administrator;
                    break;
                case "Menadzer":
                    Uloga = Uloge.Menadzer;
                    break;
            }
            switch (pol)
            {
                case "Musko":
                    Pol = Polovi.Musko;
                    break;
                case "Zensko":
                    Pol = Polovi.Zensko;
                    break;
            }
        }


    }
}
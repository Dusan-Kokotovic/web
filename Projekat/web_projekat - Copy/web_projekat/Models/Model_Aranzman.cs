using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_projekat.Models
{
    public enum Tipovi_Aranzmana
    {
        Nocenje_Sa_Doruckom, Polupansion, Pun_Pansion, All_Inclusive, Najam_Apartmana
    }
    public enum Tipovi_Prevoza
    {
        Autobus, Avion, Autobus_Avion, Individualan, Ostalo
    }

    public class Model_Aranzman
    {
        public string Id { get; set; }
        public string Naziv { get; set; }
        public Tipovi_Aranzmana Tip_Aranzmana { get; set; }
        public Tipovi_Prevoza Tip_Prevoza { get; set; }
        public string Lokacija { get; set; }
        public string Datum_Pocetka { get; set; }
        public string Datum_Zavrsetka { get; set; }
        public string Adresa_Nalazenja { get; set; }
        public double Geografska_Sirina { get; set; }
        public double Geografska_Duzina { get; set; }
        public string Vreme_Nalazenja { get; set; }
        public int Broj_Putnika { get; set; }
        public string Opis { get; set; }
        public string Program { get; set; }
        public string Poster { get; set; }
        public string Smestaj { get; set; }
        public bool Obrisan { get; set; }
        public string Menadzer { get; set; }

        public Model_Aranzman() { }
        public Model_Aranzman(string naziv, string tip_Aranzmana, string tip_Prevoza, string lokacija, string datum_Pocetka, string datum_Kraja
            , string adresa_Nalazenja, double sirina, double duzina, string vreme, int broj, string opis, string program, string poster, string smestaj, bool obrisan, string menadzer, string id)
        {
            Id = id;
            Naziv = naziv;
            Lokacija = lokacija;
            Datum_Pocetka = datum_Pocetka;
            Datum_Zavrsetka = datum_Kraja;
            Adresa_Nalazenja = adresa_Nalazenja;
            Geografska_Sirina = sirina;
            Geografska_Duzina = duzina;
            Vreme_Nalazenja = vreme;
            Broj_Putnika = broj;
            Opis = opis;
            Program = program;
            Poster = poster;
            Smestaj = smestaj;
            Obrisan = obrisan;
            Menadzer = menadzer;
            switch (tip_Aranzmana)
            {
                case "Nocenje_Sa_Doruckom":
                    Tip_Aranzmana = Tipovi_Aranzmana.Nocenje_Sa_Doruckom;
                    break;
                case "Polupansion":
                    Tip_Aranzmana = Tipovi_Aranzmana.Polupansion;
                    break;
                case "All_Inclusive":
                    Tip_Aranzmana = Tipovi_Aranzmana.All_Inclusive;
                    break;
                case "Pun_Pansion":
                    Tip_Aranzmana = Tipovi_Aranzmana.Pun_Pansion;
                    break;
                case "Najam_Apartmana":
                    Tip_Aranzmana = Tipovi_Aranzmana.Najam_Apartmana;
                    break;
            }
            switch (tip_Prevoza)
            {
                case "Autobus":
                    Tip_Prevoza = Tipovi_Prevoza.Autobus;
                    break;
                case "Avion":
                    Tip_Prevoza = Tipovi_Prevoza.Avion;
                    break;
                case "Autobus_Avion":
                    Tip_Prevoza = Tipovi_Prevoza.Autobus_Avion;
                    break;
                case "Individualan":
                    Tip_Prevoza = Tipovi_Prevoza.Individualan;
                    break;
                case "Ostalo":
                    Tip_Prevoza = Tipovi_Prevoza.Ostalo;
                    break;
            }
        }


    }
}
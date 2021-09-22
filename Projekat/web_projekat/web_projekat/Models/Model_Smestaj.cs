using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_projekat.Models
{

    public enum Tipovi_Smestaja { Motel, Hotel, Vila }

    public class Model_Smestaj
    {
        public string Id { get; set; }
        public Tipovi_Smestaja Tip { get; set; }
        public string Naziv { get; set; }
        public int Zvezdice { get; set; }
        public bool Bazen { get; set; }
        public bool Spa { get; set; }
        public bool Prilagodjeno { get; set; }
        public bool Wifi { get; set; }
        public int Slobodno { get; set; }
        public List<string> Jedinice { get; set; }
        public bool Obrisan { get; set; }
        public string Menadzer { get; set; }

        public Model_Smestaj() { }

        public Model_Smestaj(string tip, string naziv, int zvezdice, bool bazen, bool spa, bool prilagodjeno, bool wifi, List<string> lista, bool obrisan, string menadzer, string id)
        {
            Slobodno = lista.Count();
            Id = id;
            Naziv = naziv;
            Bazen = bazen;
            Spa = spa;
            Prilagodjeno = prilagodjeno;
            Wifi = wifi;
            Jedinice = lista;
            Menadzer = menadzer;
            Zvezdice = zvezdice;
            Obrisan = obrisan;
            switch (tip)
            {
                case "Hotel":
                    Tip = Tipovi_Smestaja.Hotel;
                    Zvezdice = zvezdice;
                    break;
                case "Motel":
                    Tip = Tipovi_Smestaja.Motel;
                    break;
                case "Vila":
                    Tip = Tipovi_Smestaja.Vila;
                    break;
            }
        }

    }
}
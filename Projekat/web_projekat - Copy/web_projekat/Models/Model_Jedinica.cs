using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_projekat.Models
{
    public class Model_Jedinica
    {

        public string Id { get; set; }
        public string Naziv { get; set; }
        public int BrojGostiju { get; set; }
        public bool Ljubimci { get; set; }
        public int Cena { get; set; }
        public bool Obisana { get; set; }
        public bool Dostupna { get; set; }

        Model_Jedinica() { }
        public Model_Jedinica(string id,int broj, bool ljubimci, int cena, string naziv, bool obrisana, bool dostupna)
        {
            Naziv = naziv;
            Id = id;
            BrojGostiju = broj;
            Ljubimci = ljubimci;
            Cena = cena;
            Obisana = obrisana;
            Dostupna = dostupna;
        }
    }
}
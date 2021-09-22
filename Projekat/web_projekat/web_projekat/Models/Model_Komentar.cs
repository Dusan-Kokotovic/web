using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_projekat.Models
{
    public class Model_Komentar
    {
        public string Id { get; set; }
        public string Turista { get; set; }
        public string Aranzman { get; set; }
        public string Tekst { get; set; }
        public int Ocena { get; set; }
        public bool Odobren { get; set; }
        public bool Obrisan { get; set; }

        public Model_Komentar() { }

        public Model_Komentar(string turista, string aranzman, string tekst, int ocena, bool odobren, string id)
        {
            Id = id;
            Turista = turista;
            Aranzman = aranzman;
            Tekst = tekst;
            Ocena = ocena;
            Odobren = odobren;
            Obrisan = false;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_projekat.Models
{

    public enum Statusi_Rezervacije { Aktivna, Otkazana }

    public class Model_Rezervacija
    {

        public string Id { get; set; }
        public string Turista { get; set; }
        public string Aranzman { get; set; }
        public string SmestajnaJedinica { get; set; }
        public Statusi_Rezervacije Status { get; set; }


        public Model_Rezervacija() { }

        public Model_Rezervacija(string id, string turista, string aranzman, string smestaj, string status)
        {
            Id = id;
            Turista = turista;
            Aranzman = aranzman;
            SmestajnaJedinica = smestaj;
            switch (status)
            {
                case "Aktivna":
                    Status = Statusi_Rezervacije.Aktivna;
                    break;
                case "Otkazana":
                    Status = Statusi_Rezervacije.Otkazana;
                    break;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Newtonsoft.Json;

namespace web_projekat.Models
{
    public class Repository
    {

        public static List<Model_Jedinica> Smestajne_Jedinice { get; set; } = new List<Model_Jedinica>();
        public static List<Model_Smestaj> Smestaji { get; set; } = new List<Model_Smestaj>();
        public static List<Model_Rezervacija> Rezervacije { get; set; } = new List<Model_Rezervacija>();
        public static List<Model_Komentar> Komentari { get; set; } = new List<Model_Komentar>();
        public static List<Model_Aranzman> Aranzmani { get; set; } = new List<Model_Aranzman>();
        public static List<Model_Korisnik> Korisnici { get; set; } = new List<Model_Korisnik>();
        #region Jedinice
        public static List<Model_Jedinica> Ucitaj_Jedinice()
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/jedinice.json");
            Smestajne_Jedinice = JsonConvert.DeserializeObject<List<Model_Jedinica>>(File.ReadAllText(putanja));
            return Smestajne_Jedinice;
        }

        public static void Dodaj_Jedinicu(Model_Jedinica jedinica)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/jedinice.json");
            Smestajne_Jedinice = JsonConvert.DeserializeObject<List<Model_Jedinica>>(File.ReadAllText(putanja));
            if (Smestajne_Jedinice == null)
            {
                Smestajne_Jedinice = new List<Model_Jedinica>();
            }
            Smestajne_Jedinice.Add(jedinica);
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Smestajne_Jedinice, Formatting.Indented));
        }

        public static void Rezervisi_Jedinicu(Model_Jedinica jedinica)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/jedinice.json");
            Smestajne_Jedinice = JsonConvert.DeserializeObject<List<Model_Jedinica>>(File.ReadAllText(putanja));
            foreach (Model_Jedinica sj in Smestajne_Jedinice)
            {
                if (sj.Naziv == jedinica.Naziv)                
                    sj.Dostupna = false;                
            }
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Smestajne_Jedinice, Formatting.Indented));
        }

        public static void Otkazi_Jedinicu(Model_Jedinica jedinica)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/jedinice.json");
            Smestajne_Jedinice = JsonConvert.DeserializeObject<List<Model_Jedinica>>(File.ReadAllText(putanja));
            foreach (Model_Jedinica sj in Smestajne_Jedinice)
            {
                if (sj.Naziv == jedinica.Naziv)
                    sj.Dostupna = true;
            }
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Smestajne_Jedinice, Formatting.Indented));
        }

        public static void Obrisi_Jedinicu(Model_Jedinica jedinica)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/jedinice.json");
            Smestajne_Jedinice = JsonConvert.DeserializeObject<List<Model_Jedinica>>(File.ReadAllText(putanja));
            foreach (Model_Jedinica sj in Smestajne_Jedinice)
            {
                if (sj.Naziv == jedinica.Naziv)
                    sj.Obisana = true;

            }
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Smestajne_Jedinice, Formatting.Indented));
        }

        public static void Izmeni_Jedinicu(Model_Jedinica jedinica)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/jedinice.json");
            Smestajne_Jedinice = JsonConvert.DeserializeObject<List<Model_Jedinica>>(File.ReadAllText(putanja));
            foreach (Model_Jedinica sj in Smestajne_Jedinice)
            {
                if (sj.Id == jedinica.Id)
                {
                    sj.Naziv = jedinica.Naziv;
                    sj.Cena = jedinica.Cena;
                    sj.BrojGostiju = jedinica.BrojGostiju;
                    sj.Ljubimci = jedinica.Ljubimci;
                }             
            }
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Smestajne_Jedinice, Formatting.Indented));
        }
        #endregion
        #region Smestaji

        public static List<Model_Smestaj> Ucitaj_Smestaj()
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/smestaji.json");
            Smestaji = JsonConvert.DeserializeObject<List<Model_Smestaj>>(File.ReadAllText(putanja));
            return Smestaji;
        }

        public static void Dodaj_Smestaj(Model_Smestaj model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/smestaji.json");
            Smestaji = JsonConvert.DeserializeObject<List<Model_Smestaj>>(File.ReadAllText(putanja));
            if (Smestaji == null)
            {
                Smestaji = new List<Model_Smestaj>();
            }
            Smestaji.Add(model);
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Smestaji, Formatting.Indented));
        }

        public static void Obrisi_Smestaj(Model_Smestaj model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/smestaji.json");
            Smestaji = JsonConvert.DeserializeObject<List<Model_Smestaj>>(File.ReadAllText(putanja));
            foreach (Model_Smestaj sm in Smestaji)
            {
                if (sm.Naziv == model.Naziv)
                    sm.Obrisan = true;                
            }
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Smestaji, Formatting.Indented));
        }

        public static void Izmeni_Smestaj(Model_Smestaj model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/smestaji.json");
            Smestaji = JsonConvert.DeserializeObject<List<Model_Smestaj>>(File.ReadAllText(putanja));
            foreach (Model_Smestaj sm in Smestaji)
            {
                if (sm.Id == model.Id)
                {
                    sm.Jedinice = model.Jedinice;
                    sm.Bazen = model.Bazen;
                    sm.Menadzer = model.Menadzer;
                    sm.Naziv = model.Naziv;
                    sm.Prilagodjeno = model.Prilagodjeno;
                    sm.Spa = model.Spa;
                    sm.Tip = model.Tip;
                    sm.Wifi = model.Wifi;
                    sm.Zvezdice = model.Zvezdice;
                }
            }
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Smestaji, Formatting.Indented));

        }

        #endregion
        #region Rezervacije
        public static List<Model_Rezervacija> Ucitaj_Rezervacije()
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/rezervacije.json");
            Rezervacije = JsonConvert.DeserializeObject<List<Model_Rezervacija>>(File.ReadAllText(putanja));
            return Rezervacije;
        }

        public static void Dodaj_Rezervaciju(Model_Rezervacija model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/rezervacije.json");
            Rezervacije = JsonConvert.DeserializeObject<List<Model_Rezervacija>>(File.ReadAllText(putanja));
            if (Rezervacije == null)
            {
                Rezervacije = new List<Model_Rezervacija>();
            }
            Rezervacije.Add(model);
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Rezervacije, Formatting.Indented));
        }

        public static void Otkazi_Rezervaciju(Model_Rezervacija model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/rezervacije.json");
            Rezervacije = JsonConvert.DeserializeObject<List<Model_Rezervacija>>(File.ReadAllText(putanja));
            foreach (Model_Rezervacija rezerv in Rezervacije)
            {
                if (rezerv.Id == model.Id)
                    rezerv.Status = Statusi_Rezervacije.Otkazana;                
            }
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Rezervacije, Formatting.Indented));
        }

        #endregion
        #region Komentari

        public static List<Model_Komentar> Ucitaj_Komentare()
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/komentari.json");
            Komentari = JsonConvert.DeserializeObject<List<Model_Komentar>>(File.ReadAllText(putanja));
            return Komentari;
        }

        public static void Dodaj_Komentar(Model_Komentar model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/komentari.json");
            Komentari = JsonConvert.DeserializeObject<List<Model_Komentar>>(File.ReadAllText(putanja));
            if (Komentari == null)
            {
                Komentari = new List<Model_Komentar>();
            }
            Komentari.Add(model);
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Komentari, Formatting.Indented));
        }

        public static void Odobri_Komentar(Model_Komentar model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/komentari.json");
            Komentari = JsonConvert.DeserializeObject<List<Model_Komentar>>(File.ReadAllText(putanja));
            foreach (Model_Komentar kom in Komentari)
            {
                if (kom.Id == model.Id)
                    kom.Odobren= true;            
            }
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Komentari, Formatting.Indented));
        }
        public static void Obrisi_Komentar(Model_Komentar model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/komentari.json");
            Komentari = JsonConvert.DeserializeObject<List<Model_Komentar>>(File.ReadAllText(putanja));
            foreach (Model_Komentar kom in Komentari)
            {
                if (kom.Id == model.Id)
                    kom.Obrisan = true;
            }
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Komentari, Formatting.Indented));
        }


        #endregion
        #region Aranzmani
        public static List<Model_Aranzman> Ucitaj_Aranzmane()
        {
            
            string putanja = HostingEnvironment.MapPath("~/App_Data/aranzmani.json");
            Aranzmani = JsonConvert.DeserializeObject<List<Model_Aranzman>>(File.ReadAllText(putanja));
            return Aranzmani;
        }

        public static void  Dodaj_Aranzman(Model_Aranzman model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/aranzmani.json");
            Aranzmani = JsonConvert.DeserializeObject<List<Model_Aranzman>>(File.ReadAllText(putanja));
            if (Aranzmani == null)
            {
                Aranzmani = new List<Model_Aranzman>();
            }
            Aranzmani.Add(model);
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Aranzmani,Formatting.Indented));
        }

        public static void Obrisi_Aranzman(Model_Aranzman model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/aranzmani.json");
            Aranzmani = JsonConvert.DeserializeObject<List<Model_Aranzman>>(File.ReadAllText(putanja));
            foreach (Model_Aranzman ara in Aranzmani)
            {
                if (ara.Naziv == model.Naziv)
                {
                    ara.Obrisan = true;
                }
            }
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Aranzmani, Formatting.Indented));
        }

        public static void Izmeni_Aranzman(Model_Aranzman model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/aranzmani.json");
            Aranzmani = JsonConvert.DeserializeObject<List<Model_Aranzman>>(File.ReadAllText(putanja));
            foreach (Model_Aranzman ara in Aranzmani)
            {
                if (ara.Id == model.Id)
                {
                    ara.Adresa_Nalazenja = model.Adresa_Nalazenja;
                    ara.Broj_Putnika = model.Broj_Putnika;
                    ara.Datum_Pocetka = model.Datum_Pocetka;
                    ara.Datum_Zavrsetka = model.Datum_Zavrsetka;
                    ara.Geografska_Duzina = model.Geografska_Duzina;
                    ara.Geografska_Sirina = model.Geografska_Sirina;
                    ara.Lokacija = model.Lokacija;
                    ara.Menadzer = model.Menadzer;
                    ara.Naziv = model.Naziv;
                    ara.Opis = model.Opis;
                    ara.Poster = model.Poster;
                    ara.Program = model.Program;
                    ara.Smestaj = model.Smestaj;
                    ara.Tip_Aranzmana = model.Tip_Aranzmana;
                    ara.Tip_Prevoza = model.Tip_Prevoza;
                    ara.Vreme_Nalazenja = model.Vreme_Nalazenja;
                }
            }
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Aranzmani, Formatting.Indented));
        }

        #endregion
        #region Korisnici
        public static List<Model_Korisnik> Ucitaj_Korisnike()
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.json");
            Korisnici = JsonConvert.DeserializeObject<List<Model_Korisnik>>(File.ReadAllText(putanja));
            return Korisnici;
        }

        public static void Dodaj_Korisnika(Model_Korisnik model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.json");
            Korisnici = JsonConvert.DeserializeObject<List<Model_Korisnik>>(File.ReadAllText(putanja));
            if(Korisnici == null)
            {
                Korisnici = new List<Model_Korisnik>();
            }
            Korisnici.Add(model);
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Korisnici, Formatting.Indented));

        }

        public static void Uredi_Korisnika(Model_Korisnik model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.json");
            Korisnici = JsonConvert.DeserializeObject<List<Model_Korisnik>>(File.ReadAllText(putanja));

            foreach (Model_Korisnik k in Korisnici)
            {
                if (k.Korisnicko_Ime == model.Korisnicko_Ime)
                {
                    k.Ime = model.Ime;
                    k.Prezime = model.Prezime;
                    k.Pol = model.Pol;
                    k.Lozinka = model.Lozinka;
                    k.Datum_Rodjenja = model.Datum_Rodjenja;
                    k.Email = model.Email;
                    k.Aranzmani = model.Aranzmani;
                    k.Otkazivanja = model.Otkazivanja;
                }
            }
            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Korisnici, Formatting.Indented));

        }

        public static void Obrisi_Korisnika(Model_Korisnik model)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.json");
            Korisnici = JsonConvert.DeserializeObject<List<Model_Korisnik>>(File.ReadAllText(putanja));

            foreach (Model_Korisnik k in Korisnici)
            {
                if (model.Korisnicko_Ime == k.Korisnicko_Ime)
                    k.Obrisan = true;
            }

            File.WriteAllText(putanja, string.Empty);
            File.WriteAllText(putanja, JsonConvert.SerializeObject(Korisnici, Formatting.Indented));
        }



        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Text.Json;


namespace web_projekat.Models
{
    public class Repository
    {
        
        List<Model_Jedinica> Smestajne_Jedinice { get; set; }
        List<Model_Smestaj> Smestaji { get; set;}
        List<Model_Rezervacija> Rezervacije { get; set; }
        List<Model_Komentar> Komentari { get; set; }
        List<Model_Aranzman> Aranzmani { get; set; }
        List<Model_Korisnik> Korisnici { get; set; }
        #region Jedinice
        public List<Model_Jedinica> Ucitaj_Jedinice()
        {
            Smestajne_Jedinice = new List<Model_Jedinica>();
            string putanja = HostingEnvironment.MapPath("~/App_Data/smestajnejedinice.csv");
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red="";
            while ((red = sr.ReadLine()) != null)
            {
                string[] part = red.Split('%');
                Model_Jedinica model = new Model_Jedinica(part[6],int.Parse(part[0]), bool.Parse(part[1]), int.Parse(part[2]), part[3], bool.Parse(part[4]), bool.Parse(part[5]));
                Smestajne_Jedinice.Add(model);
            }
            sr.Close();
            stream.Close();
            return Smestajne_Jedinice;
        }

        public void Dodaj_Jedinicu(Model_Jedinica jedinica)
        {
            Smestajne_Jedinice.Add(jedinica);
            string putanja = HostingEnvironment.MapPath("~/App_Data/smestajnejedinice.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Jedinica jed in Smestajne_Jedinice)
                {
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}",jed.Id, jed.BrojGostiju.ToString(), jed.Ljubimci.ToString(), jed.Cena.ToString(),
                        jed.Naziv, jed.Obisana.ToString(), jed.Dostupna.ToString());
                }
            }
        }

        public void Rezervisi_Jedinicu(Model_Jedinica jedinica)
        {
            foreach (Model_Jedinica sj in Smestajne_Jedinice)
            {
                if (sj.Naziv == jedinica.Naziv)                
                    sj.Dostupna = false;                
            }
            string putanja = HostingEnvironment.MapPath("~/App_Data/smestajnejedinice.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Jedinica jed in Smestajne_Jedinice)
                {
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}", jed.Id, jed.BrojGostiju.ToString(), jed.Ljubimci.ToString(), jed.Cena.ToString(),
                        jed.Naziv, jed.Obisana.ToString(), jed.Dostupna.ToString());
                }
            }
        }

        public void Otkazi_Jedinicu(Model_Jedinica jedinica)
        {
            foreach (Model_Jedinica sj in Smestajne_Jedinice)
            {
                if (sj.Naziv == jedinica.Naziv)
                    sj.Dostupna = true;
            }
            string putanja = HostingEnvironment.MapPath("~/App_Data/smestajnejedinice.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Jedinica jed in Smestajne_Jedinice)
                {
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}", jed.Id, jed.BrojGostiju.ToString(), jed.Ljubimci.ToString(), jed.Cena.ToString(),
                        jed.Naziv, jed.Obisana.ToString(), jed.Dostupna.ToString());
                }
            }
        }

        public void Obrisi_Jedinicu(Model_Jedinica jedinica)
        {
            foreach (Model_Jedinica sj in Smestajne_Jedinice)
            {
                if (sj.Naziv == jedinica.Naziv)
                    sj.Obisana = true;

            }
            string putanja = HostingEnvironment.MapPath("~/App_Data/smestajnejedinice.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Jedinica jed in Smestajne_Jedinice)
                {
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}", jed.Id, jed.BrojGostiju.ToString(), jed.Ljubimci.ToString(), jed.Cena.ToString(),
                        jed.Naziv, jed.Obisana.ToString(), jed.Dostupna.ToString());
                }
            }
        }

        public void Izmeni_Jedinicu(Model_Jedinica jedinica)
        {
            foreach (Model_Jedinica sj in Smestajne_Jedinice)
            {
                if (sj.Id == jedinica.Id)
                    sj.Naziv = jedinica.Naziv;
                    sj.Cena = jedinica.Cena;
                    sj.BrojGostiju = jedinica.BrojGostiju;
                    sj.Ljubimci = jedinica.Ljubimci;             
            }
            string putanja = HostingEnvironment.MapPath("~/App_Data/smestajnejedinice.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Jedinica jed in Smestajne_Jedinice)
                {
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}", jed.Id, jed.BrojGostiju.ToString(), jed.Ljubimci.ToString(), jed.Cena.ToString(),
                        jed.Naziv, jed.Obisana.ToString(), jed.Dostupna.ToString());
                }
            }
        }
        #endregion
        #region Smestaji

        public List<Model_Smestaj> Ucitaj_Smestaj()
        {
            Smestaji = new List<Model_Smestaj>();
            string putanja = HostingEnvironment.MapPath("~/App_Data/smestaji.csv");
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";
            while ((red = sr.ReadLine()) != null)
            {
                List<string> lista = new List<string>();
                string[] part = red.Split('%');
                string[] jedinice = part[7].Split('#');
                foreach (string s in jedinice)
                {
                    lista.Add(s);
                }
                Model_Smestaj smes = new Model_Smestaj(part[0], part[1], int.Parse(part[2]), bool.Parse(part[3]), bool.Parse(part[4]),
                 bool.Parse(part[5]), bool.Parse(part[6]), lista, bool.Parse(part[8]), part[9], part[10]);


                Smestaji.Add(smes);
            }
            sr.Close();
            stream.Close();

            return Smestaji;
        }

        public void Dodaj_Smestaj(Model_Smestaj model)
        {
            Smestaji.Add(model);
            string putanja = HostingEnvironment.MapPath("~/App_Data/smestaji.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Smestaj sm in Smestaji)
                {
                    string jed = "";
                    foreach (string s in sm.Jedinice)
                    {
                        jed += s;
                        if (sm.Jedinice.IndexOf(s) < sm.Jedinice.Count - 1)
                            jed += "#";                        
                    }
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}%{7}%{8}%{9}%{10}", sm.Tip.ToString(), sm.Naziv, sm.Zvezdice.ToString(),
                        sm.Bazen.ToString(), sm.Spa.ToString(), sm.Prilagodjeno.ToString(), sm.Wifi.ToString(), jed, sm.Obrisan.ToString(), sm.Menadzer, sm.Id);
                }
            }
        }

        public void Obrisi_Smestaj(Model_Smestaj model)
        {
            foreach (Model_Smestaj sm in Smestaji)
            {
                if (sm.Naziv == model.Naziv)
                    sm.Obrisan = true;                
            }

            string putanja = HostingEnvironment.MapPath("~/App_Data/smestaji.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Smestaj sm in Smestaji)
                {
                    string jed = "";
                    foreach (string s in sm.Jedinice)
                    {
                        jed += s;
                        if (sm.Jedinice.IndexOf(s) < sm.Jedinice.Count - 1)
                            jed += "#";
                    }
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}%{7}%{8}%{9}%{10}", sm.Tip.ToString(), sm.Naziv, sm.Zvezdice.ToString(),
                        sm.Bazen.ToString(), sm.Spa.ToString(), sm.Prilagodjeno.ToString(), sm.Wifi.ToString(), jed, sm.Obrisan.ToString(), sm.Menadzer, sm.Id);
                }
            }
        }

        public void Izmeni_Smestaj(Model_Smestaj model)
        {
            foreach (Model_Smestaj sm in Smestaji)
            {
                if (sm.Id == model.Id)
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

            string putanja = HostingEnvironment.MapPath("~/App_Data/smestaji.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Smestaj sm in Smestaji)
                {
                    string jed = "";
                    foreach (string s in sm.Jedinice)
                    {
                        jed += s;
                        if (sm.Jedinice.IndexOf(s) < sm.Jedinice.Count - 1)
                            jed += "#";
                    }
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}%{7}%{8}%{9}%{10}", sm.Tip.ToString(), sm.Naziv, sm.Zvezdice.ToString(),
                        sm.Bazen.ToString(), sm.Spa.ToString(), sm.Prilagodjeno.ToString(), sm.Wifi.ToString(), jed, sm.Obrisan.ToString(), sm.Menadzer, sm.Id);
                }
            }
        }

        #endregion
        #region Rezervacije
        public List<Model_Rezervacija> Ucitaj_Rezervacije()
        {
            Rezervacije = new List<Model_Rezervacija>();
            string putanja = HostingEnvironment.MapPath("~/App_Data/rezervacije.csv");
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";
            while ((red = sr.ReadLine()) != null)
            {
                string[] tokens = red.Split('%');
                Model_Rezervacija rez = new Model_Rezervacija(tokens[0], tokens[1], tokens[2], tokens[3], tokens[4]);
                Rezervacije.Add(rez);
            }
            sr.Close();
            stream.Close();
            return Rezervacije;
        }

        public void Dodaj_Rezervaciju(Model_Rezervacija model)
        {
            Rezervacije.Add(model);
            string path = HostingEnvironment.MapPath("~/App_Data/rezervacije.csv");
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (Model_Rezervacija rez in Rezervacije)
                {
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}", rez.Id, rez.Turista, rez.Aranzman, rez.SmestajnaJedinica, rez.Status.ToString());
                }
            }
        }

        public void Otkazi_Rezervaciju(Model_Rezervacija model)
        {
            foreach (Model_Rezervacija rezerv in Rezervacije)
            {
                if (rezerv.Id == model.Id)
                    rezerv.Status = Statusi_Rezervacije.Otkazana;                
            }
            string path = HostingEnvironment.MapPath("~/App_Data/rezervacije.csv");
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (Model_Rezervacija rez in Rezervacije)
                {
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}", rez.Id, rez.Turista, rez.Aranzman, rez.SmestajnaJedinica, rez.Status.ToString());
                }
            }
        }

        #endregion
        #region Komentari

        public List<Model_Komentar> Ucitaj_Komentare()
        {
            Komentari = new List<Model_Komentar>();
            string putanja = HostingEnvironment.MapPath("~/App_Data/komentari.csv");
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";
            while ((red = sr.ReadLine()) != null)
            {
                string[] part = red.Split('%');
                Model_Komentar k = new Model_Komentar(part[0], part[1], part[2], int.Parse(part[3]), bool.Parse(part[4]), part[5]);
                Komentari.Add(k);
            }
            sr.Close();
            stream.Close();
            return Komentari;
        }

        public void Dodaj_Komentar(Model_Komentar model)
        {
            Komentari.Add(model);
            string putanja = HostingEnvironment.MapPath("~/App_Data/komentari.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Komentar kom in Komentari)
                {
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}", kom.Turista, kom.Aranzman, kom.Tekst, kom.Ocena.ToString(), kom.Odobren.ToString(), kom.Id);
                }
            }
        }

        public void Odobri_Komentar(Model_Komentar model)
        {
            foreach (Model_Komentar kom in Komentari)
            {
                if (kom.Id == model.Id)
                    kom.Odobren
 = true;            
            }
            string putanja = HostingEnvironment.MapPath("~/App_Data/komentari.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Komentar kom in Komentari)
                {
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}", kom.Turista, kom.Aranzman, kom.Tekst, kom.Ocena.ToString(), kom.Odobren.ToString(), kom.Id);
                }
            }
        }


        #endregion
        #region Aranzmani
        public List<Model_Aranzman> Ucitaj_Aranzmane()
        {
            Aranzmani = new List<Model_Aranzman>();
            string putanja = HostingEnvironment.MapPath("~/App_Data/aranzmani.csv");
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";
            while ((red = sr.ReadLine()) != null)
            {
                string[] part = red.Split('%');
                Model_Aranzman ar = new Model_Aranzman(part[0], part[1], part[2], part[3], part[4], part[5], part[6], double.Parse(part[7]),
                                    double.Parse(part[8]), part[9], int.Parse(part[10]), part[11], part[12], part[13], part[14], bool.Parse(part[15]), part[16], part[17]);
                Aranzmani.Add(ar);

            }
            sr.Close();
            stream.Close();
            return Aranzmani;
        }

        public void Dodaj_Aranzman(Model_Aranzman model)
        {
            Aranzmani.Add(model);
            string putanja = HostingEnvironment.MapPath("~/App_Data/aranzmani.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Aranzman ar in Aranzmani)
                {
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}%{7}%{8}%{9}%{10}%{11}%{12}%{13}%{14}%{15}%{16}%{17}", ar.Naziv, ar.Tip_Aranzmana.ToString(), ar.Tip_Prevoza.ToString(),
                    ar.Lokacija, ar.Datum_Pocetka, ar.Datum_Zavrsetka, ar.Adresa_Nalazenja, ar.Geografska_Sirina.ToString(), ar.Geografska_Duzina.ToString(), ar.Vreme_Nalazenja,
                    ar.Broj_Putnika.ToString(), ar.Opis, ar.Program, ar.Poster, ar.Smestaj, ar.Obrisan.ToString(), ar.Menadzer, ar.Id);
                }
            }
        }

        public void Obrisi_Aranzman(Model_Aranzman model)
        {
            foreach (Model_Aranzman ara in Aranzmani)
            {
                if (ara.Naziv == model.Naziv)
                {
                    ara.Obrisan = true;
                }
            }
            string putanja = HostingEnvironment.MapPath("~/App_Data/aranzmani.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Aranzman ar in Aranzmani)
                {
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}%{7}%{8}%{9}%{10}%{11}%{12}%{13}%{14}%{15}%{16}%{17}", ar.Naziv, ar.Tip_Aranzmana.ToString(), ar.Tip_Prevoza.ToString(),
                    ar.Lokacija, ar.Datum_Pocetka, ar.Datum_Zavrsetka, ar.Adresa_Nalazenja, ar.Geografska_Sirina.ToString(), ar.Geografska_Duzina.ToString(), ar.Vreme_Nalazenja,
                    ar.Broj_Putnika.ToString(), ar.Opis, ar.Program, ar.Poster, ar.Smestaj, ar.Obrisan.ToString(), ar.Menadzer, ar.Id);
                }
            }
        }

        public void Izmeni_Aranzman(Model_Aranzman model)
        {

            foreach (Model_Aranzman ara in Aranzmani)
            {
                if (ara.Id == model.Id)
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
            string putanja = HostingEnvironment.MapPath("~/App_Data/aranzmani.csv");
                using (StreamWriter writer = new StreamWriter(putanja))
                {
                    foreach (Model_Aranzman ar in Aranzmani)
                    {
                        writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}%{7}%{8}%{9}%{10}%{11}%{12}%{13}%{14}%{15}%{16}%{17}", ar.Naziv, ar.Tip_Aranzmana.ToString(), ar.Tip_Prevoza.ToString(),
                        ar.Lokacija, ar.Datum_Pocetka, ar.Datum_Zavrsetka, ar.Adresa_Nalazenja, ar.Geografska_Sirina.ToString(), ar.Geografska_Duzina.ToString(), ar.Vreme_Nalazenja,
                        ar.Broj_Putnika.ToString(), ar.Opis, ar.Program, ar.Poster, ar.Smestaj, ar.Obrisan.ToString(), ar.Menadzer, ar.Id);
                    }
                }
            
        }

        #endregion
        #region Korisnici
        public List<Model_Korisnik> Ucitaj_Korisnike()
        {
            Korisnici = new List<Model_Korisnik>();
            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.csv");
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";
            while ((red = sr.ReadLine()) != null)
            {
                List<string> lista = new List<string>();
                string[] part = red.Split('%');
                string[] aranzmani = part[9].Split('#');
                foreach (string s in aranzmani)
                {
                    lista.Add(s);
                }
                Model_Korisnik kor = new Model_Korisnik(part[0], part[1], part[2], part[3], part[4], part[5], part[6], part[7], bool.Parse(part[8]), lista, int.Parse(part[10]));
                Korisnici.Add(kor);
            }
            sr.Close();
            stream.Close();

            return Korisnici;
        }

        public void Dodaj_Korisnika(Model_Korisnik model)
        {
            Korisnici.Add(model);
            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Korisnik kor in Korisnici)
                {
                    string aranz = "";
                    foreach (string s in kor.Aranzmani)
                    {
                        aranz += s;
                        if (kor.Aranzmani.IndexOf(s) < kor.Aranzmani.Count - 1)
                            aranz += "#";
                    }
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}%{7}%{8}%{9}%{10}", kor.Korisnicko_Ime, kor.Lozinka, kor.Ime, kor.Prezime, kor.Pol, kor.Email, kor.Datum_Rodjenja, kor.Uloga, kor.Obrisan.ToString(), aranz, kor.Otkazivanja);
                }
            }
        }

        public void Uredi_Korisnika(Model_Korisnik model)
        {
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
            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Korisnik kor in Korisnici)
                {
                    string aranz = "";
                    foreach (string s in kor.Aranzmani)
                    {
                        aranz += s;
                        if (kor.Aranzmani.IndexOf(s) < kor.Aranzmani.Count - 1)
                            aranz += "#";
                    }
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}%{7}%{8}%{9}%{10}", kor.Korisnicko_Ime, kor.Lozinka, kor.Ime, kor.Prezime, kor.Pol, kor.Email, kor.Datum_Rodjenja, kor.Uloga, kor.Obrisan.ToString(), aranz, kor.Otkazivanja);
                }
            }
        }

        public void Obrisi_Korisnika(Model_Korisnik model)
        {
            foreach (Model_Korisnik k in Korisnici)
            {
                if (model.Korisnicko_Ime == k.Korisnicko_Ime)
                    k.Obrisan = true;
            }
            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.csv");
            using (StreamWriter writer = new StreamWriter(putanja))
            {
                foreach (Model_Korisnik kor in Korisnici)
                {
                    string aranz = "";
                    foreach (string s in kor.Aranzmani)
                    {
                        aranz += s;
                        if (kor.Aranzmani.IndexOf(s) < kor.Aranzmani.Count - 1)
                            aranz += "#";
                    }
                    writer.WriteLine("{0}%{1}%{2}%{3}%{4}%{5}%{6}%{7}%{8}%{9}%{10}", kor.Korisnicko_Ime, kor.Lozinka, kor.Ime, kor.Prezime, kor.Pol, kor.Email, kor.Datum_Rodjenja, kor.Uloga, kor.Obrisan.ToString(), aranz, kor.Otkazivanja);
                }
            }
        }



        #endregion
    }
}
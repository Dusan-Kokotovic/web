using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_projekat.Models;

namespace web_projekat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Aranzman> bag = new List<Model_Aranzman>();
            foreach (Model_Aranzman a in aranzmani)
            {
                DateTime datum = DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                if ((DateTime.Compare(DateTime.Now, datum) < 0) && a.Obrisan == false)
                    bag.Add(a);
            }

            bag.Sort(
            delegate (Model_Aranzman a1, Model_Aranzman a2)
            {
                return DateTime.ParseExact(a1.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a2.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));
            }
            );
            HttpContext.Application["aranzmani"] = bag;
            Model_Korisnik korisnik = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            if(korisnik == null)
            {
                return RedirectToAction("Index_Neprijavljeni", "Neprijavljeni");

            }
            switch (korisnik.Uloga)
            {
                case Models.Uloge.Administrator:
                    return RedirectToAction("Index_Administrator", "Administrator");
                 //   break;
                case Models.Uloge.Turista:
                    return RedirectToAction("Index_Turista", "Turista");
                //    break;
                case Models.Uloge.Menadzer:
                    return RedirectToAction("Index_Menadzer", "Menadzer");
                  //  break;
            }
            return RedirectToAction("Index_Neprijavljeni", "Neprijavljeni");
        }

        public ActionResult Tekuci_Aranzmani()
        {
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Aranzman> bag = new List<Model_Aranzman>();
            foreach (Model_Aranzman a in aranzmani)
            {
                DateTime datum = DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                if ((DateTime.Compare(DateTime.Now, datum) > 0) && a.Obrisan == false)
                    bag.Add(a);
            }

            bag.Sort(
            delegate (Model_Aranzman a1, Model_Aranzman a2)
            {
                return DateTime.ParseExact(a2.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a1.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));

            }
            );
            HttpContext.Application["aranzmani"] = bag;
            Model_Korisnik korisnik = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            if (korisnik == null)
            {
                return RedirectToAction("Tekuci_Neprijavljeni", "Neprijavljeni");
            }
            switch (korisnik.Uloga)
            {
                case Models.Uloge.Administrator:
                    return RedirectToAction("Tekuci_Administrator", "Administrator");
                  //  break;
                case Models.Uloge.Turista:
                    return RedirectToAction("Tekuci_Turista", "Turista");
                  //  break;
                case Models.Uloge.Menadzer:
                    return RedirectToAction("Tekuci_Menadzer", "Menadzer");
                   // break;
            }
            return RedirectToAction("Tekuci_Neprijavljeni", "Neprijavljeni");

        }

        public ActionResult Detalji(string Naziv)
        {
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            Model_Aranzman aranzman = new Model_Aranzman();
            foreach (Model_Aranzman a in aranzmani)
            {
                if (a.Naziv == Naziv)
                {
                    aranzman = a;
                }
            }
            HttpContext.Application["aranzman"] = aranzman;
            Model_Korisnik korisnik = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            if (korisnik == null)
            {
                return RedirectToAction("Detalji_Neprijavljeni", "Neprijavljeni");
            }
            switch (korisnik.Uloga)
            {
                case Models.Uloge.Administrator:
                    return RedirectToAction("Detalji_Administrator", "Administrator");
                   // break;
                case Models.Uloge.Turista:
                    return RedirectToAction("Detalji_Turista", "Turista");
                  //  break;
                case Models.Uloge.Menadzer:
                    return RedirectToAction("Detalji_Menadzer", "Menadzer");
                    //              break;
            }
            return RedirectToAction("Detalji_Neprijavljeni", "Neprijavljeni");


        }
        public ActionResult PrikazSmestaj(string Naziv)
        {
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            List<Model_Jedinica> smjed = Models.Repository.Ucitaj_Jedinice();
            List<Model_Jedinica> jedinice = new List<Model_Jedinica>();
            Model_Smestaj smestaj = new Model_Smestaj();
            HttpContext.Application["ZaSortJedinica"] = Naziv;
            foreach (Model_Smestaj s in smestaji)
            {
                if (s.Naziv == Naziv)
                    smestaj = s;
            }
            foreach (Model_Jedinica m in smjed)
            {
                foreach (string jedinica in smestaj.Jedinice)
                {

                    if (m.Naziv == jedinica && m.Obisana == false)
                        jedinice.Add(m);
                }
            }

            ViewModel vm = new ViewModel(jedinice, smestaj);
            HttpContext.Application["smestaj"] = vm;
            Model_Korisnik korisnik = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            if (korisnik == null)
            {
                return RedirectToAction("Smestaj_Neprijavljeni", "Neprijavljeni");
            }
            switch (korisnik.Uloga)
            {
                case Models.Uloge.Administrator:
                    return RedirectToAction("Smestaj_Administrator", "Administrator");
                    //break;
                case Models.Uloge.Turista:
                    return RedirectToAction("Smestaj_Turista", "Turista");
                   // break;
                case Models.Uloge.Menadzer:
                    return RedirectToAction("Smestaj_Menadzer", "Menadzer");
                  //  break;
            }
            return RedirectToAction("Smestaj_Neprijavljeni", "Neprijavljeni");
        }
        public ActionResult Jedinice(string Naziv)
        {
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            List<Model_Jedinica> smjed = Models.Repository.Ucitaj_Jedinice();
            List<Model_Jedinica> jedinice = new List<Model_Jedinica>();
            Model_Smestaj sm = new Model_Smestaj();
            HttpContext.Application["ZaSortJedinica"] = Naziv;
            foreach (Model_Smestaj ss in smestaji)
            {
                if (ss.Naziv == Naziv)
                    sm = ss;
            }

            foreach (Model_Jedinica m in smjed)
            {
                foreach (string jedinica in sm.Jedinice)
                {

                    if (m.Naziv == jedinica && m.Obisana == false)
                        jedinice.Add(m);
                }
            }

            HttpContext.Application["jedinice"] = jedinice;
            Model_Korisnik korisnik = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            if (korisnik == null)
            {
                return RedirectToAction("Jedinice_Neprijavljeni", "Neprijavljeni");
            }
            switch (korisnik.Uloga)
            {
                case Models.Uloge.Administrator:
                    return RedirectToAction("Jedinice_Administrator", "Administrator");
                    //break;
                case Models.Uloge.Turista:
                    return RedirectToAction("Jedinice_Turista", "Turista");
                   // break;
                case Models.Uloge.Menadzer:
                    return RedirectToAction("Jedinice_Menadzer", "Menadzer");
                   // break;
            }
            return RedirectToAction("Jedinice_Neprijavljeni", "Neprijavljeni");
        }

        public ActionResult Utisci()
        {
            List<Model_Komentar> komentar = Models.Repository.Ucitaj_Komentare();
            List<Model_Komentar> utisci = new List<Model_Komentar>();

            foreach (Model_Komentar k in komentar)
            {
                if (k.Odobren == true && k.Obrisan == false)               
                    utisci.Add(k);               
            }

            HttpContext.Application["utisci"] = utisci;
            Model_Korisnik korisnik = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            if (korisnik == null)
            {
                return RedirectToAction("Utisci_Neprijavljeni", "Neprijavljeni");
            }
            switch (korisnik.Uloga)
            {
                case Models.Uloge.Administrator:
                    return RedirectToAction("Utisci_Administrator", "Administrator");
                    //break;
                case Models.Uloge.Turista:
                    return RedirectToAction("Utisci_Turista", "Turista");
                   // break;
                case Models.Uloge.Menadzer:
                    return RedirectToAction("Utisci_Menadzer", "Menadzer");
                   // break;
            }
            return RedirectToAction("Utisci_Neprijavljeni", "Neprijavljeni");
        }
        public ActionResult SignIn()
        {
            return View("Registracija");
        }

        public ActionResult Registracija(Model_Korisnik korisnik)
        {
            List<Model_Korisnik> korisnici = Models.Repository.Ucitaj_Korisnike();

            foreach (Model_Korisnik k in korisnici)
            {
                if (k.Korisnicko_Ime == korisnik.Korisnicko_Ime && k.Obrisan == false)
                {
                    return View("Registracija");

                }
            }
            DateTime datum = DateTime.Parse(korisnik.Datum_Rodjenja);
            korisnik.Datum_Rodjenja = datum.ToString("dd/MM/yyyy");
            korisnik.Aranzmani = new List<string>();
            //korisnik.Uloga = Uloge.Turista;
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            if (prijavljeni != null)
            {
                if (prijavljeni.Uloga == Uloge.Administrator)
                {
                    korisnik.Uloga = Uloge.Menadzer;
                    Models.Repository.Dodaj_Korisnika(korisnik);
                    return RedirectToAction("Index", "Home");
                }
            }
            Models.Repository.Dodaj_Korisnika(korisnik);
            HttpContext.Session["Prijavljeni"] = korisnik;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login(string Korisnicko_Ime,string Lozinka)
        {
            List<Model_Korisnik> lista = Models.Repository.Ucitaj_Korisnike();
            foreach (Model_Korisnik k in lista)
            {
                if (k.Korisnicko_Ime == Korisnicko_Ime && k.Lozinka == Lozinka && k.Obrisan == false)
                {
                    HttpContext.Session["Prijavljeni"] = k;
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SortAranzmanaPredstojecih(string Parametar,string Red)
        {
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Aranzman> bag = new List<Model_Aranzman>();
            foreach (Model_Aranzman a in aranzmani)
            {
                DateTime datum = DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                if ((DateTime.Compare(DateTime.Now, datum) < 0) && a.Obrisan == false)
                    bag.Add(a);
            }

            switch (Parametar)
            {
                case "Naziv":
                    if(Red == "Rastuce")
                    {
                        bag.Sort(
                                    delegate (Model_Aranzman a1, Model_Aranzman a2)
                                    {
                                        return a1.Naziv.CompareTo(a2.Naziv);
                                    }
                                    );
                    }
                    else
                    {
                        bag.Sort(
                                    delegate (Model_Aranzman a1, Model_Aranzman a2)
                                    {
                                        return a2.Naziv.CompareTo(a1.Naziv);
                                    }
                                    );
                    }
                    break;
                case "DatumKraja":
                    if (Red == "Rastuce")
                    {                       
                                bag.Sort(
                                delegate (Model_Aranzman a1, Model_Aranzman a2)
                                {
                                    return DateTime.ParseExact(a1.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a2.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));                
                                }
                                );
                    }
                    else
                    {
                        bag.Sort(
                                delegate (Model_Aranzman a1, Model_Aranzman a2)
                                {
                                    return DateTime.ParseExact(a2.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a1.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                                }
                                );
                    }
                    break;
                case "DatumPocetka":
                    if (Red == "Rastuce")
                    {
                        bag.Sort(
                        delegate (Model_Aranzman a1, Model_Aranzman a2)
                        {
                            return DateTime.ParseExact(a1.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a2.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                        }
                        );
                    }
                    else
                    {
                        bag.Sort(
                                delegate (Model_Aranzman a1, Model_Aranzman a2)
                                {
                                    return DateTime.ParseExact(a2.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a1.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                                }
                                );
                    }
                    break;
            }
            HttpContext.Application["aranzmani"] = bag;
            Model_Korisnik korisnik = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            if (korisnik == null)
            {
                return RedirectToAction("Index_Neprijavljeni", "Neprijavljeni");

            }
            switch (korisnik.Uloga)
            {
                case Models.Uloge.Administrator:
                    return RedirectToAction("Index_Administrator", "Administrator");
                    //break;
                case Models.Uloge.Turista:
                    return RedirectToAction("Index_Turista", "Turista");
                   // break;
                case Models.Uloge.Menadzer:
                    return RedirectToAction("Index_Menadzer", "Menadzer");
                  //  break;
            }
            return RedirectToAction("Index_Neprijavljeni", "Neprijavljeni");
        }
        public ActionResult SortAranzmanaTekucih(string Parametar,string Red)
        {
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Aranzman> bag = new List<Model_Aranzman>();
            foreach (Model_Aranzman a in aranzmani)
            {
                DateTime datum = DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                if ((DateTime.Compare(DateTime.Now, datum) > 0) && a.Obrisan == false)
                    bag.Add(a);
            }


            switch (Parametar)
            {
                case "Naziv":
                    if (Red == "Rastuce")
                    {
                        bag.Sort(
                                    delegate (Model_Aranzman a1, Model_Aranzman a2)
                                    {
                                        return a1.Naziv.CompareTo(a2.Naziv);
                                    }
                                    );
                    }
                    else
                    {
                        bag.Sort(
                                    delegate (Model_Aranzman a1, Model_Aranzman a2)
                                    {
                                        return a2.Naziv.CompareTo(a1.Naziv);
                                    }
                                    );
                    }
                    break;
                case "DatumKraja":
                    if (Red == "Rastuce")
                    {
                        bag.Sort(
                        delegate (Model_Aranzman a1, Model_Aranzman a2)
                        {
                            return DateTime.ParseExact(a1.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a2.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                        }
                        );
                    }
                    else
                    {
                        bag.Sort(
                                delegate (Model_Aranzman a1, Model_Aranzman a2)
                                {
                                    return DateTime.ParseExact(a2.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a1.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                                }
                                );
                    }
                    break;
                case "DatumPocetka":
                    if (Red == "Rastuce")
                    {
                        bag.Sort(
                        delegate (Model_Aranzman a1, Model_Aranzman a2)
                        {
                            return DateTime.ParseExact(a1.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a2.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                        }
                        );
                    }
                    else
                    {
                        bag.Sort(
                                delegate (Model_Aranzman a1, Model_Aranzman a2)
                                {
                                    return DateTime.ParseExact(a2.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a1.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                                }
                                );
                    }
                    break;
            }


            HttpContext.Application["aranzmani"] = bag;
            Model_Korisnik korisnik = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            if (korisnik == null)
            {
                return RedirectToAction("Tekuci_Neprijavljeni", "Neprijavljeni");

            }
            switch (korisnik.Uloga)
            {
                case Models.Uloge.Administrator:
                    return RedirectToAction("Tekuci_Administrator", "Administrator");
                    //break;
                case Models.Uloge.Turista:
                    return RedirectToAction("Tekuci_Turista", "Turista");
                   // break;
                case Models.Uloge.Menadzer:
                    return RedirectToAction("Tekuci_Menadzer", "Menadzer");
                   // break;
            }
            return RedirectToAction("Tekuci_Neprijavljeni", "Neprijavljeni");
        }
        
        public ActionResult SortJedinica(string Parametar,string Red)
        {
            List<Model_Jedinica> jed = Models.Repository.Ucitaj_Jedinice();
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            string naziv = (string)HttpContext.Application["ZaSortJedinica"];
            Model_Smestaj smestaj = new Model_Smestaj();
            List<Model_Jedinica> jedinice = new List<Model_Jedinica>();

            foreach (Model_Smestaj s in smestaji)
            {
                if (s.Naziv == naziv)
                    smestaj = s;
            }
            foreach (Model_Jedinica m in jed)
            {
                foreach (string jedinica in smestaj.Jedinice)
                {

                    if (m.Naziv == jedinica && m.Obisana == false)
                        jedinice.Add(m);
                }
            }


            switch (Parametar)
            {
                case "Cena":
                    if(Red == "Rastuce")
                    {
                        jedinice.Sort(
                            delegate (Model_Jedinica s1, Model_Jedinica s2)
                            {
                                return s1.Cena.CompareTo(s2.Cena);
                            }
                             );
                    }
                    else
                    {
                        jedinice.Sort(
                            delegate (Model_Jedinica s1, Model_Jedinica s2)
                            {
                                return s2.Cena.CompareTo(s1.Cena);
                            }
                             );
                    }
                    break;
                case "Kapacitet":
                    if (Red == "Rastuce")
                    {
                        jedinice.Sort(
                            delegate (Model_Jedinica s1, Model_Jedinica s2)
                            {
                                return s1.BrojGostiju.CompareTo(s2.BrojGostiju);
                            }
                             );
                    }
                    else
                    {
                        jedinice.Sort(
                            delegate (Model_Jedinica s1, Model_Jedinica s2)
                            {
                                return s2.BrojGostiju.CompareTo(s1.BrojGostiju);
                            }
                             );
                    }
                    break;
            }
            ViewModel vm = new ViewModel(jedinice, smestaj);
            HttpContext.Application["smestaj"] = vm;
            Model_Korisnik korisnik = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            if (korisnik == null)
            {
                return RedirectToAction("Smestaj_Neprijavljeni", "Neprijavljeni");
            }
            switch (korisnik.Uloga)
            {
                case Models.Uloge.Administrator:
                    return RedirectToAction("Smestaj_Administrator", "Administrator");
                   // break;
                case Models.Uloge.Turista:
                    return RedirectToAction("Smestaj_Turista", "Turista");
                  //  break;
                case Models.Uloge.Menadzer:
                    return RedirectToAction("Smestaj_Menadzer", "Menadzer");
                //    break;
            }
            return RedirectToAction("Smestaj_Neprijavljeni", "Neprijavljeni");
        }


        public ActionResult SortKorisnika(string Parametar,string Red)
        {
            List<Model_Korisnik> kor = Models.Repository.Ucitaj_Korisnike();
            List<Model_Korisnik> korisnici = new List<Model_Korisnik>();
            foreach (Model_Korisnik model in kor)
            {
                if (model.Obrisan == false)
                    korisnici.Add(model);
            }


            switch (Parametar)
            {
                case "Ime":
                    if (Red == "Rastuce")
                    {
                        korisnici.Sort(
                            delegate (Model_Korisnik k1, Model_Korisnik k2)
                            {
                                return k1.Ime.CompareTo(k2.Ime);
                            }
                            );
                    }
                    else
                    {
                        korisnici.Sort(
                            delegate (Model_Korisnik k1, Model_Korisnik k2)
                            {
                                return k2.Ime.CompareTo(k1.Ime);
                            }
                            );
                    }
                    break;
                case "Prezime":
                    if (Red == "Rastuce")
                    {
                        korisnici.Sort(
                            delegate (Model_Korisnik k1, Model_Korisnik k2)
                            {
                                return k1.Prezime.CompareTo(k2.Prezime);
                            }
                            );
                    }
                    else
                    {
                        korisnici.Sort(
                            delegate (Model_Korisnik k1, Model_Korisnik k2)
                            {
                                return k2.Prezime.CompareTo(k1.Prezime);
                            }
                            );
                    }
                    break;
                case "Uloga":
                    if (Red == "Rastuce")
                    {
                        korisnici.Sort(
                            delegate (Model_Korisnik k1, Model_Korisnik k2)
                            {
                                return k1.Uloga.CompareTo(k2.Uloga);
                            }
                            );
                    }
                    else
                    {
                        korisnici.Sort(
                            delegate (Model_Korisnik k1, Model_Korisnik k2)
                            {
                                return k2.Uloga.CompareTo(k1.Uloga);
                            }
                            );
                    }
                    break;
            }
            HttpContext.Application["korisnici"] = korisnici;

            return RedirectToAction("Svi_Administrator", "Administrator");
        }

        public ActionResult Svi_Korisnici()
        {
            List<Model_Korisnik> svi = Models.Repository.Ucitaj_Korisnike();
            List<Model_Korisnik> neobrisani = new List<Model_Korisnik>();
            foreach (Model_Korisnik model in svi)
            {
                if (model.Obrisan == false)
                    neobrisani.Add(model);          
            }
            HttpContext.Application["korisnici"] = neobrisani;
            return RedirectToAction("Svi_Administrator", "Administrator");
        }
        public ActionResult Sumnjivi_Korisnici()
        {
            List<Model_Korisnik> svi = Models.Repository.Ucitaj_Korisnike();
            List<Model_Korisnik> neobrisani = new List<Model_Korisnik>();
            foreach (Model_Korisnik model in svi)
            {
                if (model.Obrisan == false && model.Otkazivanja >=2)
                    neobrisani.Add(model);
            }
            HttpContext.Application["sumnjivi"] = neobrisani;
            return RedirectToAction("Sumnjivi_Administrator", "Administrator");
        }

        public ActionResult Blokiraj_korisnika(string Korisnicko_Ime)
        {
            List<Model_Korisnik> svi = Models.Repository.Ucitaj_Korisnike();
            foreach(Model_Korisnik korisnik in svi)
            {
                if (korisnik.Korisnicko_Ime == Korisnicko_Ime)
                    Models.Repository.Obrisi_Korisnika(korisnik);
            }
            return RedirectToAction("Sumnjivi_Korisnici");

        }

        public ActionResult Profil()
        {
            ViewBag.prikaz = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            return View();
        }

        public ActionResult Izmena(Model_Korisnik korisnik)
        {
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            switch (prijavljeni.Uloga)
            {
                case Models.Uloge.Administrator:
                    korisnik.Uloga = Uloge.Administrator;
                   break;
                case Models.Uloge.Turista:
                    korisnik.Uloga = Uloge.Turista;
                    break;
                case Models.Uloge.Menadzer:
                    korisnik.Uloga = Uloge.Menadzer;
                    break;
            }
            string[] lista = korisnik.Datum_Rodjenja.Split('-');
            string datum = lista[2] + "/"+ lista[1] + "/"+ lista[0];
            korisnik.Datum_Rodjenja = datum;
            korisnik.Aranzmani = prijavljeni.Aranzmani;
            Models.Repository.Uredi_Korisnika(korisnik);
            HttpContext.Session["Prijavljeni"] = korisnik;


            switch (prijavljeni.Uloga)
            {
                case Models.Uloge.Administrator:
                    return RedirectToAction("Index_Administrator", "Administrator");
                    //break;
                case Models.Uloge.Turista:
                    return RedirectToAction("Index_Turista", "Turista");
                   // break;
                case Models.Uloge.Menadzer:
                    return RedirectToAction("Index_Menadzer", "Menadzer");
                  //  break;
            }
            return RedirectToAction("Index_Neprijavljeni", "Neprijavljeni");

        }

        public ActionResult Rezervisi(string Naziv_Jedinice)
        {
            List<Model_Jedinica> lista_jedinica = Models.Repository.Ucitaj_Jedinice();
            List<Model_Korisnik> lista_korisnika = Models.Repository.Ucitaj_Korisnike();
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];

            foreach (Model_Jedinica j in lista_jedinica)
            {
                if (j.Naziv == Naziv_Jedinice)                
                    Models.Repository.Rezervisi_Jedinicu(j);
               
            }
            Model_Aranzman aranzman = (Model_Aranzman)HttpContext.Application["aranzman"];
            string straranzman = aranzman.Naziv;
            prijavljeni.Aranzmani.Add(straranzman);
            Models.Repository.Uredi_Korisnika(prijavljeni);

            const string chars = "qwertyuiopasdf";
            var rand = new Random();
            var kod = new string(Enumerable.Repeat(chars, 7).Select(s => s[rand.Next(s.Length)]).ToArray());

            Model_Rezervacija rezervacija = new Model_Rezervacija(kod, prijavljeni.Korisnicko_Ime, straranzman, Naziv_Jedinice, "Aktivna");
            Models.Repository.Dodaj_Rezervaciju(rezervacija);

            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            List<Model_Jedinica> jedinice = new List<Model_Jedinica>();
            Model_Smestaj smestaj = new Model_Smestaj();
            foreach (Model_Smestaj ss in smestaji)
            {
                if (ss.Naziv == aranzman.Smestaj)
                     smestaj = ss;
            }
            lista_jedinica = Models.Repository.Ucitaj_Jedinice();

            foreach (Model_Jedinica m in lista_jedinica)
            {
                foreach (string jedinica in smestaj.Jedinice)
                {

                    if (m.Naziv == jedinica && m.Obisana == false)
                        jedinice.Add(m);
                }
            }
            ViewModel vm = new ViewModel(jedinice, smestaj);
            HttpContext.Application["smestaj"] = vm;
            return RedirectToAction("Smestaj_Turista", "Turista");
        }

        public ActionResult MojeRezervacije()
        {
            List<Model_Rezervacija> lista_rezervacija = Models.Repository.Ucitaj_Rezervacije();
            List<Model_Rezervacija> prikaz = new List<Model_Rezervacija>();
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            foreach (Model_Rezervacija rez in lista_rezervacija)
            {
                if (rez.Turista == prijavljeni.Korisnicko_Ime)                
                    prikaz.Add(rez);                
            }
            HttpContext.Application["rezervacije"] = prikaz;
            return RedirectToAction("Rezervacije_Turista", "Turista");
        }

        public ActionResult SortRezervacija(string Parametar,string Red)
        {
            List<Model_Rezervacija> lista = (List<Model_Rezervacija>)HttpContext.Application["rezervacije"];
            switch (Parametar)
            {
                case "Id":
                    if (Red == "Rastuce")
                    {
                        lista.Sort(
                            delegate (Model_Rezervacija k1, Model_Rezervacija k2)
                            {
                                return k1.Id.CompareTo(k2.Id);
                            }
                            );
                    }
                    else
                    {
                        lista.Sort(
                            delegate (Model_Rezervacija k1, Model_Rezervacija k2)
                            {
                                return k2.Id.CompareTo(k1.Id);
                            }
                            );
                    }
                    break;
                case "Aranzman":
                    if (Red == "Rastuce")
                    {
                        lista.Sort(
                            delegate (Model_Rezervacija k1, Model_Rezervacija k2)
                            {
                                return k1.Aranzman.CompareTo(k2.Aranzman);
                            }
                            );
                    }
                    else
                    {
                        lista.Sort(
                            delegate (Model_Rezervacija k1, Model_Rezervacija k2)
                            {
                                return k2.Aranzman.CompareTo(k1.Aranzman);
                            }
                            );
                    }
                    break;
                case "Jedinica":
                    if (Red == "Rastuce")
                    {
                        lista.Sort(
                            delegate (Model_Rezervacija k1, Model_Rezervacija k2)
                            {
                                return k1.SmestajnaJedinica.CompareTo(k2.SmestajnaJedinica);
                            }
                            );
                    }
                    else
                    {
                        lista.Sort(
                            delegate (Model_Rezervacija k1, Model_Rezervacija k2)
                            {
                                return k2.SmestajnaJedinica.CompareTo(k1.SmestajnaJedinica);
                            }
                            );
                    }
                    break;
                case "Status":
                    if (Red == "Rastuce")
                    {
                        lista.Sort(
                            delegate (Model_Rezervacija k1, Model_Rezervacija k2)
                            {
                                return k1.Status.CompareTo(k2.Status);
                            }
                            );
                    }
                    else
                    {
                        lista.Sort(
                            delegate (Model_Rezervacija k1, Model_Rezervacija k2)
                            {
                                return k2.Status.CompareTo(k1.Status);
                            }
                            );
                    }
                    break;
            }
            HttpContext.Application["rezervacije"] = lista;
            return RedirectToAction("Rezervacije_Turista", "Turista");
        }

        public ActionResult Otkazi(string Aranzman,string Id,string Jedinica)
        {
            List<Model_Jedinica> jedinice = Models.Repository.Ucitaj_Jedinice();
            List<Model_Rezervacija> rezervacije = Models.Repository.Ucitaj_Rezervacije();
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];


            foreach (Model_Aranzman a in aranzmani)
            {
                DateTime datum = DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture);

                if (a.Obrisan == false && a.Naziv == Aranzman && (DateTime.Compare(DateTime.Now, datum) > 0))
                    return RedirectToAction("Rezervacije_Turista", "Turista");
            }

            foreach (Model_Rezervacija rez in rezervacije)
            {
                if (rez.Id == Id)
                    Models.Repository.Otkazi_Rezervaciju(rez);
            }

            foreach (Model_Jedinica j in jedinice)
            {
                if (j.Naziv == Jedinica)
                    Models.Repository.Otkazi_Jedinicu(j);                
            }
            prijavljeni.Otkazivanja++;
            Models.Repository.Uredi_Korisnika(prijavljeni);
            return RedirectToAction("MojeRezervacije");
        }

        public ActionResult DodajKomentar()
        {
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Rezervacija> rezervacije = Models.Repository.Ucitaj_Rezervacije();
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            List<Model_Rezervacija> protekli = new List<Model_Rezervacija>();
            List<Model_Komentar> komentari = Models.Repository.Ucitaj_Komentare();
            foreach (Model_Aranzman a in aranzmani)
            {
                foreach(Model_Rezervacija r in rezervacije)
                {
                    DateTime datum = DateTime.ParseExact(a.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                    if (a.Naziv == r.Aranzman && (DateTime.Compare(DateTime.Now, datum) > 0) & r.Turista == prijavljeni.Korisnicko_Ime && r.Status == Statusi_Rezervacije.Aktivna)                        
                            protekli.Add(r);                  
                }
            }           
            HttpContext.Application["protekli"] = protekli;
            return RedirectToAction("DodajKomentar_Turista", "Turista");

        }

        public ActionResult Okaci(string Turista, string Aranzman, int Zvezdice, string Komentar)
        {
            const string chars = "qwertyuiopasdf";
            var rand = new Random();
            var kod = new string(Enumerable.Repeat(chars, 7).Select(s => s[rand.Next(s.Length)]).ToArray());
            Model_Komentar komentar = new Model_Komentar(Turista, Aranzman, Komentar, Zvezdice, false, kod);
            Models.Repository.Dodaj_Komentar(komentar);
            return RedirectToAction("Index_Turista", "Turista");
        }

        public ActionResult KomentariNaCekanju()
        {
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Komentar> komentari = Models.Repository.Ucitaj_Komentare();
            List<Model_Komentar> bag = new List<Model_Komentar>();
            foreach(Model_Aranzman a in aranzmani)
            {
                foreach(Model_Komentar k in komentari)
                {
                    if(a.Naziv == k.Aranzman && a.Menadzer == prijavljeni.Korisnicko_Ime)
                    {
                        bag.Add(k);
                    }
                }
            }

            HttpContext.Application["komentari_na_cekanju"] = bag;
            return RedirectToAction("KomentariNaCekanju_Menadzer", "Menadzer");

        }

        public ActionResult OdobriKomentar(string Id)
        {
            List<Model_Komentar> komentari = Models.Repository.Ucitaj_Komentare();
            Model_Komentar taj = new Model_Komentar();
            foreach (Model_Komentar k in komentari)
            {
                if (k.Id == Id)
                    taj = k;
            }
            Models.Repository.Odobri_Komentar(taj);
            return RedirectToAction("KomentariNaCekanju");
        }
        public ActionResult ObrisiKomentar(string Id)
        {
            List<Model_Komentar> komentari = Models.Repository.Ucitaj_Komentare();
            Model_Komentar taj = new Model_Komentar();
            foreach (Model_Komentar k in komentari)
            {
                if (k.Id == Id)
                    taj = k;
            }
            Models.Repository.Obrisi_Komentar(taj);
            return RedirectToAction("KomentariNaCekanju");
        }
        public ActionResult RezervacijeMojih()
        {
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Rezervacija> rezervacije = Models.Repository.Ucitaj_Rezervacije();
            List<Model_Rezervacija> bag = new List<Model_Rezervacija>();
            foreach(Model_Aranzman a in aranzmani)
            {
                foreach(Model_Rezervacija r in rezervacije)
                {
                    if (a.Menadzer == prijavljeni.Korisnicko_Ime && r.Aranzman == a.Naziv)
                        bag.Add(r);
                }
            }

            HttpContext.Application["rezervacije_mojih"] = bag;
            return RedirectToAction("RezervacijeMojih_Menadzer", "Menadzer");
        }
        public ActionResult DetaljiRezervacije(Model_Rezervacija rezervacija)
        {
            HttpContext.Application["prikaz_rezervacija"] = rezervacija;
            return RedirectToAction("PrikazRezervacije_Menadzer", "Menadzer");
        }

        public ActionResult Aranzmani()
        {
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Aranzman> bag = new List<Model_Aranzman>();

            foreach(Model_Aranzman a in aranzmani)
            {
                if (a.Menadzer == prijavljeni.Korisnicko_Ime && a.Obrisan == false)
                    bag.Add(a);
            }
            HttpContext.Application["menadzer_aranzmani"] = bag;
            return RedirectToAction("Aranzmani_Menadzer", "Menadzer");
        }

        public ActionResult SortAranzmanaMenadzera(string Parametar, string Red)
        {
           
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            List<Model_Aranzman> bag = new List<Model_Aranzman>();
                foreach (Model_Aranzman a in aranzmani)
                {
                    if (a.Menadzer == prijavljeni.Korisnicko_Ime && a.Obrisan == false)
                        bag.Add(a);
                }

                switch (Parametar)
                {
                    case "Naziv":
                        if (Red == "Rastuce")
                        {
                            bag.Sort(
                                        delegate (Model_Aranzman a1, Model_Aranzman a2)
                                        {
                                            return a1.Naziv.CompareTo(a2.Naziv);
                                        }
                                        );
                        }
                        else
                        {
                            bag.Sort(
                                        delegate (Model_Aranzman a1, Model_Aranzman a2)
                                        {
                                            return a2.Naziv.CompareTo(a1.Naziv);
                                        }
                                        );
                        }
                        break;
                    case "DatumKraja":
                        if (Red == "Rastuce")
                        {
                            bag.Sort(
                            delegate (Model_Aranzman a1, Model_Aranzman a2)
                            {
                                return DateTime.ParseExact(a1.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a2.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                            }
                            );
                        }
                        else
                        {
                            bag.Sort(
                                    delegate (Model_Aranzman a1, Model_Aranzman a2)
                                    {
                                        return DateTime.ParseExact(a2.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a1.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                                    }
                                    );
                        }
                        break;
                    case "DatumPocetka":
                        if (Red == "Rastuce")
                        {
                            bag.Sort(
                            delegate (Model_Aranzman a1, Model_Aranzman a2)
                            {
                                return DateTime.ParseExact(a1.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a2.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                            }
                            );
                        }
                        else
                        {
                            bag.Sort(
                                    delegate (Model_Aranzman a1, Model_Aranzman a2)
                                    {
                                        return DateTime.ParseExact(a2.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(a1.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                                    }
                                    );
                        }
                        break;
                }
            HttpContext.Application["menadzer_aranzmani"] = bag;
            return RedirectToAction("Aranzmani_Menadzer", "Menadzer");

        }

        public ActionResult DodajAranzman(Model_Aranzman aranzman)
        {
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            DateTime datum_pocetka = DateTime.Parse(aranzman.Datum_Pocetka);
            aranzman.Datum_Pocetka = datum_pocetka.ToString("dd/MM/yyyy");
            DateTime datum_kraja = DateTime.Parse(aranzman.Datum_Zavrsetka);
            aranzman.Datum_Zavrsetka = datum_kraja.ToString("dd/MM/yyyy");
            DateTime Vreme = DateTime.Parse(aranzman.Vreme_Nalazenja);
            aranzman.Vreme_Nalazenja = Vreme.ToString("HH:mm");
            aranzman.Poster = "~/Photos/" + aranzman.Poster;
            aranzman.Menadzer = prijavljeni.Korisnicko_Ime;
            if (DateTime.Compare(datum_pocetka, datum_kraja) > 0)
            {
                return RedirectToAction("Aranzmani_Menadzer", "Menadzer");
            }
            const string chars = "qwertyuiopasdf";
            var rand = new Random();
            var kod = new string(Enumerable.Repeat(chars, 7).Select(s => s[rand.Next(s.Length)]).ToArray());
            aranzman.Id = kod;

            foreach(Model_Smestaj s in smestaji)
            {
                if(s.Naziv == aranzman.Smestaj)
                    Models.Repository.Dodaj_Aranzman(aranzman);
            }

            return RedirectToAction("Aranzmani");
        }

        public ActionResult DetaljiMenadzerov(string Naziv)
        {
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            Model_Aranzman aranzman = new Model_Aranzman();
            foreach (Model_Aranzman a in aranzmani)
            {
                if (a.Naziv == Naziv)
                {
                    aranzman = a;
                }
            }
            HttpContext.Application["aranzman_menadzerov"] = aranzman;
            return RedirectToAction("DetaljiMenadzerov_Menadzer", "Menadzer");
        }
        public ActionResult BrisiAranzman(string Naziv)
        {
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Rezervacija> rezervacije = Models.Repository.Ucitaj_Rezervacije();
            Model_Aranzman bag = new Model_Aranzman();
            foreach (Model_Aranzman a in aranzmani)
            {
                if (a.Naziv == Naziv)
                    bag = a;
                
            }
            foreach (Model_Rezervacija r in rezervacije)
            {
                if (r.Aranzman == bag.Naziv && r.Status == Statusi_Rezervacije.Aktivna)                
                    return RedirectToAction("Aranzmani");                
            }
            Models.Repository.Obrisi_Aranzman(bag);
            return RedirectToAction("Aranzmani");
        }
        public ActionResult IzmeniAranzman(string Naziv)
        {
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            foreach (Model_Aranzman a in aranzmani)
            {
                if (a.Naziv == Naziv)
                    HttpContext.Application["aranzman_za_izmenu"] = a;
            }
            return RedirectToAction("Izmena_Aranzmana_Menadzer","Menadzer");
        }

        public ActionResult ExecuteAranzman(Model_Aranzman aranzman)
        {
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            DateTime datum_pocetka = DateTime.Parse(aranzman.Datum_Pocetka);
            aranzman.Datum_Pocetka = datum_pocetka.ToString("dd/MM/yyyy");
            DateTime datum_kraja = DateTime.Parse(aranzman.Datum_Zavrsetka);
            aranzman.Datum_Zavrsetka = datum_kraja.ToString("dd/MM/yyyy");
            DateTime Vreme = DateTime.Parse(aranzman.Vreme_Nalazenja);
            aranzman.Vreme_Nalazenja = Vreme.ToString("HH:mm");
            aranzman.Menadzer = prijavljeni.Korisnicko_Ime;
            Model_Aranzman ar = (Model_Aranzman)HttpContext.Application["aranzman_za_izmenu"];
            aranzman.Id = ar.Id;
            if (aranzman.Poster == null)
            {
                aranzman.Poster = ar.Poster;
            }
            else
            {
                aranzman.Poster = "~/Photos/" + aranzman.Poster;

            }

            if (DateTime.Compare(datum_pocetka, datum_kraja) > 0)            
                return RedirectToAction("Aranzmani");      
            
            Models.Repository.Izmeni_Aranzman(aranzman);

            return RedirectToAction("Aranzmani");
        }

        public ActionResult SmestajiAdvanced()
        {
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            List<Model_Smestaj> bag = new List<Model_Smestaj>();
            foreach(Model_Smestaj s in smestaji)
            {
                if (s.Obrisan == false)
                    bag.Add(s);
            }
            HttpContext.Application["smestaji_advanced"] = bag;
            return RedirectToAction("SmestajiAdvanced_Menadzer", "Menadzer");
        }

        public ActionResult DodajSmestaj(Model_Smestaj smestaj)
        {
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            smestaj.Jedinice = new List<string>();
            smestaj.Menadzer = prijavljeni.Korisnicko_Ime;
            const string chars = "qwertyuiopasdf";
            var rand = new Random();
            var kod = new string(Enumerable.Repeat(chars, 7).Select(s => s[rand.Next(s.Length)]).ToArray());
            smestaj.Id = kod;
            Models.Repository.Dodaj_Smestaj(smestaj);
            return RedirectToAction("SmestajiAdvanced");
        }
        public ActionResult ObrisiSmestaj(string Naziv)
        {
            Model_Smestaj izabrani = new Model_Smestaj();
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Jedinica> jedinice = Models.Repository.Ucitaj_Jedinice();
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();

            foreach (Model_Aranzman a in aranzmani)
            {
                if(a.Smestaj == Naziv && DateTime.Compare(DateTime.Now, DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture)) < 0)
                    return RedirectToAction("SmestajiAdvanced");
            }
            foreach(Model_Smestaj s in smestaji)
            {
                if (s.Naziv == Naziv)
                    izabrani = s;

            }

            foreach(Model_Jedinica j in jedinice)
            {
                foreach(string jedinica in izabrani.Jedinice)
                {
                    if (jedinica == j.Naziv)
                        Models.Repository.Obrisi_Jedinicu(j);
                }
            }
            Models.Repository.Obrisi_Smestaj(izabrani);
            return RedirectToAction("SmestajiAdvanced");

        }

        public ActionResult IzmeniSmestaj(string Naziv)
        {
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            Model_Smestaj smestaj = new Model_Smestaj();
            foreach(Model_Smestaj s in smestaji)
            {
                if (s.Naziv == Naziv)
                    HttpContext.Application["smestaj_za_izmenu"] = s;
            }
            return RedirectToAction("IzmenaSmestaja_Menadzer", "Menadzer");
        }

        public ActionResult ExecuteSmestaj(Model_Smestaj smestaj)
        {
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            List<Model_Jedinica> jedinice = Models.Repository.Ucitaj_Jedinice();
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            Model_Smestaj trenutni = (Model_Smestaj)HttpContext.Application["smestaj_za_izmenu"];
            List<string> novaLista = new List<string>();
            smestaj.Id = trenutni.Id;
            smestaj.Jedinice = trenutni.Jedinice;
            smestaj.Menadzer = prijavljeni.Korisnicko_Ime;
            foreach (string s in trenutni.Jedinice)
            {
                foreach (Model_Jedinica j in jedinice)
                {

                    if (j.Naziv == s)
                    {
                        string[] parts = s.Split('-');
                        novaLista.Add(smestaj.Naziv + "-" + parts[1]);
                        j.Naziv = smestaj.Naziv + "-" + parts[1];
                        Models.Repository.Izmeni_Jedinicu(j);
                    }

                }
            }
            smestaj.Jedinice = novaLista;
            foreach (Model_Aranzman a in aranzmani)
            {
                if (a.Smestaj == trenutni.Naziv)
                {
                    a.Smestaj = smestaj.Naziv;
                    Models.Repository.Izmeni_Aranzman(a);
                }
            }
            Models.Repository.Izmeni_Smestaj(smestaj);
            return RedirectToAction("SmestajiAdvanced");
        }

        public ActionResult JediniceSmestajne(string Naziv)
        {
            List<Model_Jedinica> jedinice = Models.Repository.Ucitaj_Jedinice();
            List<Model_Jedinica> bag = new List<Model_Jedinica>();
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            HttpContext.Application["ZaSortJedinica"] = Naziv;
            Model_Smestaj smestaj = new Model_Smestaj();
            if(Naziv == null)
            {
                Naziv = (string)HttpContext.Cache["temp"];
                HttpContext.Application["ZaSortJedinica"] = Naziv;

            }
            foreach (Model_Smestaj s in smestaji)
            {
                if (s.Naziv == Naziv)
                    smestaj = s;
                
            }
            HttpContext.Cache["zaNazivSmestaja"] = smestaj;
            foreach (Model_Jedinica jedinica in jedinice)
            {
                foreach (string j in smestaj.Jedinice)
                {

                    if (j == jedinica.Naziv && jedinica.Obisana == false)
                        bag.Add(jedinica);


                }
            }
            HttpContext.Application["jedinice_za_prikaz"] = bag;
            return RedirectToAction("JediniceAdvanced_Menadzer", "Menadzer");

        }
        public ActionResult ObrisiJedinicu(string Naziv)
        {
            List<Model_Jedinica> jedinice = Models.Repository.Ucitaj_Jedinice();
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            string[] part = Naziv.Split('-');
            string smestaj = part[0];
            HttpContext.Cache["temp"] = smestaj;
            foreach (Model_Jedinica j in jedinice)
            {
                if(j.Naziv == Naziv)
                {
                    if(j.Dostupna == false)
                    {
                        return RedirectToAction("JediniceSmestajne");
                    }
                   Models.Repository.Obrisi_Jedinicu(j);
                }
            }
            foreach (Model_Smestaj s in smestaji)
            {
                if (s.Naziv == smestaj)
                {
                    s.Jedinice.Remove(Naziv);
                    Models.Repository.Izmeni_Smestaj(s);
                }
            }
            return RedirectToAction("JediniceSmestajne");
        }

        public ActionResult DodajJedinicu(Model_Jedinica jedinica)
        {
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            Model_Smestaj sm = (Model_Smestaj)HttpContext.Cache["zaNazivSmestaja"];
            string smestaj = sm.Naziv;
            HttpContext.Cache["temp"] = smestaj;
            foreach (Model_Smestaj s in smestaji)
            {
                if (s.Naziv == smestaj)
                {
                    s.Jedinice.Add(smestaj + "-" + jedinica.Naziv);
                    Models.Repository.Izmeni_Smestaj(s);
                }
            }
            const string chars = "qwertyuiopasdf";
            var rand = new Random();
            var kod = new string(Enumerable.Repeat(chars, 7).Select(s => s[rand.Next(s.Length)]).ToArray());
            jedinica.Id = kod;
            jedinica.Naziv = smestaj + "-" + jedinica.Naziv;
            jedinica.Dostupna = true;
            Models.Repository.Dodaj_Jedinicu(jedinica);
            return RedirectToAction("JediniceSmestajne");
        }

        public ActionResult IzmeniJedinicu(string Naziv)
        {
            List<Model_Jedinica> jedinice = Models.Repository.Ucitaj_Jedinice();
            Model_Jedinica bag = new Model_Jedinica();
            foreach (Model_Jedinica j in jedinice)
            {
                if (j.Naziv == Naziv)
                    bag = j;
            }
            HttpContext.Application["jedinica_za_izmenu"] = bag;
            return RedirectToAction("IzmenaJediniceMenadzer","Menadzer");
        }
        public ActionResult ExecuteJedinica(Model_Jedinica jedinica)
        {
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            Model_Jedinica trenutna = (Model_Jedinica)HttpContext.Application["jedinica_za_izmenu"];
            Model_Smestaj sm = (Model_Smestaj)HttpContext.Cache["zaNazivSmestaja"];
            HttpContext.Cache["temp"] = sm.Naziv;

            if (trenutna.Dostupna == false)
                jedinica.BrojGostiju = trenutna.BrojGostiju;


            jedinica.Id = trenutna.Id;
            jedinica.Dostupna = trenutna.Dostupna;
            jedinica.Naziv = sm.Naziv + "-" + jedinica.Naziv;


            foreach(Model_Smestaj s in smestaji)
            {
                if(s.Naziv == sm.Naziv)
                {
                    foreach(string str in s.Jedinice)
                    {
                        if(str == trenutna.Naziv)
                        {
                            s.Jedinice.RemoveAt(s.Jedinice.IndexOf(str));
                            s.Jedinice.Add(jedinica.Naziv);
                            Models.Repository.Izmeni_Smestaj(s);
                            break;
                        }
                    }
                }
            }


            Models.Repository.Izmeni_Jedinicu(jedinica);
            return RedirectToAction("JediniceSmestajne");
        }

        public ActionResult SortSmestaja(string Parametar,string Red)
        {
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            List<Model_Jedinica> jedinice = Models.Repository.Ucitaj_Jedinice();
            List<Model_Smestaj> bag = new List<Model_Smestaj>();
            foreach (Model_Smestaj s in smestaji)
            {
                if (s.Obrisan == false)
                    bag.Add(s);
            }

            switch (Parametar)
            {
                case "Naziv":
                    if (Red == "Rastuce")
                    {
                        bag.Sort(
                                    delegate (Model_Smestaj s1, Model_Smestaj s2)
                                    {
                                        return s1.Naziv.CompareTo(s2.Naziv);
                                    }
                                    );
                    }
                    else
                    {
                        bag.Sort(
                                    delegate (Model_Smestaj s1, Model_Smestaj s2)
                                    {
                                        return s2.Naziv.CompareTo(s1.Naziv);
                                    }
                                    );
                    }
                    break;
                case "Jedinice":
                    if (Red == "Rastuce")
                    {
                        bag.Sort(
                                    delegate (Model_Smestaj s1, Model_Smestaj s2)
                                    {
                                        return s1.Jedinice.Count.CompareTo(s2.Jedinice.Count);
                                    }
                                    );
                    }
                    else
                    {
                        bag.Sort(
                                    delegate (Model_Smestaj s1, Model_Smestaj s2)
                                    {
                                        return s2.Jedinice.Count.CompareTo(s1.Jedinice.Count);
                                    }
                                    );
                    }
                    break;
                case "Slobodne":
                    foreach(Model_Smestaj s in bag)
                    {
                        foreach(string str in s.Jedinice)
                        {
                            foreach(Model_Jedinica j in jedinice)
                            {
                                if (j.Naziv == str && j.Dostupna == true)
                                    s.Slobodno++;
                            }
                        }
                    }


                    if (Red == "Rastuce")
                    {
                        bag.Sort(
                                    delegate (Model_Smestaj s1, Model_Smestaj s2)
                                    {
                                        return s1.Slobodno.CompareTo(s2.Slobodno);
                                    }
                                    );
                    }
                    else
                    {
                        bag.Sort(
                                    delegate (Model_Smestaj s1, Model_Smestaj s2)
                                    {
                                        return s2.Slobodno.CompareTo(s1.Slobodno);
                                    }
                                    );
                    }
                    break;
            }
            HttpContext.Application["smestaji_advanced"] = bag;
            return RedirectToAction("SmestajiAdvanced_Menadzer", "Menadzer");
        }

        public ActionResult SortJedinicaSmestaj(string Parametar, string Red)
        {
            List<Model_Jedinica> jed = Models.Repository.Ucitaj_Jedinice();
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            string naziv = (string)HttpContext.Application["ZaSortJedinica"];
            Model_Smestaj smestaj = new Model_Smestaj();
            List<Model_Jedinica> jedinice = new List<Model_Jedinica>();

            foreach (Model_Smestaj s in smestaji)
            {
                if (s.Naziv == naziv)
                    smestaj = s;
            }
            foreach (Model_Jedinica m in jed)
            {
                foreach (string jedinica in smestaj.Jedinice)
                {

                    if (m.Naziv == jedinica && m.Obisana == false)
                        jedinice.Add(m);
                }
            }


            switch (Parametar)
            {
                case "Cena":
                    if (Red == "Rastuce")
                    {
                        jedinice.Sort(
                            delegate (Model_Jedinica s1, Model_Jedinica s2)
                            {
                                return s1.Cena.CompareTo(s2.Cena);
                            }
                             );
                    }
                    else
                    {
                        jedinice.Sort(
                            delegate (Model_Jedinica s1, Model_Jedinica s2)
                            {
                                return s2.Cena.CompareTo(s1.Cena);
                            }
                             );
                    }
                    break;
                case "Kapacitet":
                    if (Red == "Rastuce")
                    {
                        jedinice.Sort(
                            delegate (Model_Jedinica s1, Model_Jedinica s2)
                            {
                                return s1.BrojGostiju.CompareTo(s2.BrojGostiju);
                            }
                             );
                    }
                    else
                    {
                        jedinice.Sort(
                            delegate (Model_Jedinica s1, Model_Jedinica s2)
                            {
                                return s2.BrojGostiju.CompareTo(s1.BrojGostiju);
                            }
                             );
                    }
                    break;
            }
            HttpContext.Application["jedinice_za_prikaz"] = jedinice;
            return RedirectToAction("JediniceAdvanced_Menadzer", "Menadzer");
        }

        public ActionResult Logout()
        {
            HttpContext.Session["Prijavljeni"] = null;
          return  RedirectToAction("Index");
        }
    }
}
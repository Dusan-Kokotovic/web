using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_projekat.Models;

namespace web_projekat.Controllers
{
    public class PretrageController : Controller
    {
        // GET: Pretrage
        public ActionResult Aranzmani(string Naziv, string Tip_Prevoza, string Tip_Aranzmana, DateTime? Donja_Granica_Pocetak, DateTime? Gornja_Granica_Pocetak, DateTime? Donja_Granica_Kraj, DateTime? Gornja_Granica_Kraj)
        {
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Aranzman> bag = new List<Model_Aranzman>();
            List<Model_Aranzman> temp = new List<Model_Aranzman>();
            foreach (Model_Aranzman a in aranzmani)
            {
                DateTime datum = DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                if ((DateTime.Compare(DateTime.Now, datum) < 0) && a.Obrisan == false)
                    bag.Add(a);
            }

            if (!string.IsNullOrEmpty(Naziv))
            {
                bag = bag.Where(s => s.Naziv.ToLower().Contains(Naziv.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(Tip_Prevoza))
            {
                bag = bag.Where(s => s.Tip_Prevoza.ToString() == Tip_Prevoza).ToList();
            }
            if (!string.IsNullOrEmpty(Tip_Aranzmana))
            {
                bag = bag.Where(s => s.Tip_Aranzmana.ToString() == Tip_Aranzmana).ToList();
            }
            if (Donja_Granica_Pocetak != null)
            {
                foreach (Model_Aranzman a in bag)
                {
                    if (DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture) >= Donja_Granica_Pocetak)
                        temp.Add(a);
                }
                bag = temp;
            }
            if (Gornja_Granica_Pocetak != null)
            {
                temp = new List<Model_Aranzman>();
                foreach (Model_Aranzman a in bag)
                {
                    if (DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture) <= Gornja_Granica_Pocetak)
                        temp.Add(a);
                }
                bag = temp;
            }
            if (Donja_Granica_Kraj != null)
            {
                temp = new List<Model_Aranzman>();
                foreach (Model_Aranzman a in bag)
                {
                    if (DateTime.ParseExact(a.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture) >= Donja_Granica_Kraj)
                        temp.Add(a);
                }
                bag = temp;
            }
            if (Gornja_Granica_Kraj != null)
            {
                temp = new List<Model_Aranzman>();
                foreach (Model_Aranzman a in bag)
                {
                    if (DateTime.ParseExact(a.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture) <= Gornja_Granica_Kraj)
                        temp.Add(a);
                }
                bag = temp;
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

        public ActionResult AranzmaniTekuci(string Naziv, string Tip_Prevoza, string Tip_Aranzmana, DateTime? Donja_Granica_Pocetak, DateTime? Gornja_Granica_Pocetak, DateTime? Donja_Granica_Kraj, DateTime? Gornja_Granica_Kraj)
        {
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Aranzman> bag = new List<Model_Aranzman>();
            List<Model_Aranzman> temp = new List<Model_Aranzman>();
            foreach (Model_Aranzman a in aranzmani)
            {
                DateTime datum = DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                if ((DateTime.Compare(DateTime.Now, datum) > 0) && a.Obrisan == false)
                    bag.Add(a);
            }

            if (!string.IsNullOrEmpty(Naziv))
            {
                bag = bag.Where(s => s.Naziv.ToLower().Contains(Naziv.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(Tip_Prevoza))
            {
                bag = bag.Where(s => s.Tip_Prevoza.ToString() == Tip_Prevoza).ToList();
            }
            if (!string.IsNullOrEmpty(Tip_Aranzmana))
            {
                bag = bag.Where(s => s.Tip_Aranzmana.ToString() == Tip_Aranzmana).ToList();
            }
            if (Donja_Granica_Pocetak != null)
            {
                foreach (Model_Aranzman a in bag)
                {
                    if (DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture) >= Donja_Granica_Pocetak)
                        temp.Add(a);
                }
                bag = temp;
            }
            if (Gornja_Granica_Pocetak != null)
            {
                temp = new List<Model_Aranzman>();
                foreach (Model_Aranzman a in bag)
                {
                    if (DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture) <= Gornja_Granica_Pocetak)
                        temp.Add(a);
                }
                bag = temp;
            }
            if (Donja_Granica_Kraj != null)
            {
                temp = new List<Model_Aranzman>();
                foreach (Model_Aranzman a in bag)
                {
                    if (DateTime.ParseExact(a.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture) >= Donja_Granica_Kraj)
                        temp.Add(a);
                }
                bag = temp;
            }
            if (Gornja_Granica_Kraj != null)
            {
                temp = new List<Model_Aranzman>();
                foreach (Model_Aranzman a in bag)
                {
                    if (DateTime.ParseExact(a.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture) <= Gornja_Granica_Kraj)
                        temp.Add(a);
                }
                bag = temp;
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

        public ActionResult AranzmaniMenadzerovi(string Naziv, string Tip_Prevoza, string Tip_Aranzmana, DateTime? Donja_Granica_Pocetak, DateTime? Gornja_Granica_Pocetak, DateTime? Donja_Granica_Kraj, DateTime? Gornja_Granica_Kraj)
        {
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            List<Model_Aranzman> aranzmani = Models.Repository.Ucitaj_Aranzmane();
            List<Model_Aranzman> bag = new List<Model_Aranzman>();
            List<Model_Aranzman> temp = new List<Model_Aranzman>();

            foreach (Model_Aranzman a in aranzmani)
            {
                if (a.Menadzer == prijavljeni.Korisnicko_Ime && a.Obrisan == false)
                    bag.Add(a);
            }

            if (!string.IsNullOrEmpty(Naziv))
            {
                bag = bag.Where(s => s.Naziv.ToLower().Contains(Naziv.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(Tip_Prevoza))
            {
                bag = bag.Where(s => s.Tip_Prevoza.ToString() == Tip_Prevoza).ToList();
            }
            if (!string.IsNullOrEmpty(Tip_Aranzmana))
            {
                bag = bag.Where(s => s.Tip_Aranzmana.ToString() == Tip_Aranzmana).ToList();
            }
            if (Donja_Granica_Pocetak != null)
            {
                foreach (Model_Aranzman a in bag)
                {
                    if (DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture) >= Donja_Granica_Pocetak)
                        temp.Add(a);
                }
                bag = temp;
            }
            if (Gornja_Granica_Pocetak != null)
            {
                temp = new List<Model_Aranzman>();
                foreach (Model_Aranzman a in bag)
                {
                    if (DateTime.ParseExact(a.Datum_Pocetka, "dd/MM/yyyy", CultureInfo.CurrentCulture) <= Gornja_Granica_Pocetak)
                        temp.Add(a);
                }
                bag = temp;
            }
            if (Donja_Granica_Kraj != null)
            {
                temp = new List<Model_Aranzman>();
                foreach (Model_Aranzman a in bag)
                {
                    if (DateTime.ParseExact(a.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture) >= Donja_Granica_Kraj)
                        temp.Add(a);
                }
                bag = temp;
            }
            if (Gornja_Granica_Kraj != null)
            {
                temp = new List<Model_Aranzman>();
                foreach (Model_Aranzman a in bag)
                {
                    if (DateTime.ParseExact(a.Datum_Zavrsetka, "dd/MM/yyyy", CultureInfo.CurrentCulture) <= Gornja_Granica_Kraj)
                        temp.Add(a);
                }
                bag = temp;
            }
            HttpContext.Application["menadzer_aranzmani"] = bag;
            return RedirectToAction("Aranzmani_Menadzer", "Menadzer");



        }
        public ActionResult Jedinice(int? Cena, int? DonjaGranica, int? GornjaGranica, string Ljubimci)
        {
            List<Model_Jedinica> jed = Models.Repository.Ucitaj_Jedinice();
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            string naziv = (string)HttpContext.Application["ZaSortJedinica"];
            Model_Smestaj smestaj = new Model_Smestaj();
            List<Model_Jedinica> jedinice = new List<Model_Jedinica>();
            List<Model_Jedinica> temp = new List<Model_Jedinica>();

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


            if (!string.IsNullOrEmpty(Ljubimci))
            {
                bool ljub = Ljubimci == "Da" ? true : false;
                jedinice = jedinice.Where(s => s.Ljubimci == ljub).ToList();
            }

            if (DonjaGranica != null)
            {
                foreach (Model_Jedinica jedinica in jedinice)
                {
                    if (jedinica.BrojGostiju >= DonjaGranica)
                        temp.Add(jedinica);
                }
                jedinice = temp;
            }
            if (GornjaGranica != null)
            {
                temp = new List<Model_Jedinica>();
                foreach (Model_Jedinica jedinica in jedinice)
                {
                    if (jedinica.BrojGostiju <= GornjaGranica)
                    {
                        temp.Add(jedinica);
                    }
                }
                jedinice = temp;
            }

            if (Cena != null)
            {
                temp = new List<Model_Jedinica>();

                foreach (Model_Jedinica jedinica in jedinice)
                {
                    if (jedinica.Cena == Cena)
                        temp.Add(jedinica);
                }
                jedinice = temp;
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
                    // break;
            }
            return RedirectToAction("Smestaj_Neprijavljeni", "Neprijavljeni");
        }

        public ActionResult JediniceSmestaj(int? Cena, int? DonjaGranica, int? GornjaGranica, string Ljubimci)
        {
            List<Model_Jedinica> jed = Models.Repository.Ucitaj_Jedinice();
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            string naziv = (string)HttpContext.Application["ZaSortJedinica"];
            Model_Smestaj smestaj = new Model_Smestaj();
            List<Model_Jedinica> jedinice = new List<Model_Jedinica>();
            List<Model_Jedinica> temp = new List<Model_Jedinica>();

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
            if (!string.IsNullOrEmpty(Ljubimci))
            {
                bool ljub = Ljubimci == "Da" ? true : false;
                jedinice = jedinice.Where(s => s.Ljubimci == ljub).ToList();
            }

            if (DonjaGranica != null)
            {
                foreach (Model_Jedinica jedinica in jedinice)
                {
                    if (jedinica.BrojGostiju >= DonjaGranica)
                        temp.Add(jedinica);
                }
                jedinice = temp;
            }
            if (GornjaGranica != null)
            {
                temp = new List<Model_Jedinica>();
                foreach (Model_Jedinica jedinica in jedinice)
                {
                    if (jedinica.BrojGostiju <= GornjaGranica)
                    {
                        temp.Add(jedinica);
                    }
                }
                jedinice = temp;
            }

            if (Cena != null)
            {
                temp = new List<Model_Jedinica>();

                foreach (Model_Jedinica jedinica in jedinice)
                {
                    if (jedinica.Cena == Cena)
                        temp.Add(jedinica);
                }
                jedinice = temp;
            }
            HttpContext.Application["jedinice_za_prikaz"] = jedinice;
            return RedirectToAction("JediniceAdvanced_Menadzer", "Menadzer");
        }

        public ActionResult Rezervacije(string Id, string Aranzman, string SmestajnaJedinica, string Status)
        {
            List<Model_Rezervacija> lista_rezervacija = Models.Repository.Ucitaj_Rezervacije();
            List<Model_Rezervacija> prikaz = new List<Model_Rezervacija>();
            Model_Korisnik prijavljeni = (Model_Korisnik)HttpContext.Session["Prijavljeni"];
            foreach (Model_Rezervacija rez in lista_rezervacija)
            {
                if (rez.Turista == prijavljeni.Korisnicko_Ime)
                    prikaz.Add(rez);
            }
            if (!string.IsNullOrEmpty(Aranzman))
            {
                prikaz = prikaz.Where(s => s.Aranzman.ToLower().Contains(Aranzman.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(Status))
            {
                prikaz = prikaz.Where(s => s.Status.ToString().ToLower() == Status.ToLower()).ToList();
            }
            if (!string.IsNullOrEmpty(SmestajnaJedinica))
            {
                prikaz = prikaz.Where(s => s.SmestajnaJedinica.ToLower().Contains(SmestajnaJedinica.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(Id))
            {
                prikaz = prikaz.Where(s => s.Id.ToLower().Contains(Id.ToLower())).ToList();
            }

            HttpContext.Application["rezervacije"] = prikaz;
            return RedirectToAction("Rezervacije_Turista", "Turista");

        }
        public ActionResult Korisnici(string Ime, string Prezime, string Uloga)
        {
            List<Model_Korisnik> korisnici = Models.Repository.Ucitaj_Korisnike();
            List<Model_Korisnik> neobrisani = new List<Model_Korisnik>();
            foreach (Model_Korisnik model in korisnici)
            {
                if (model.Obrisan == false)
                    neobrisani.Add(model);
            }
            if (!string.IsNullOrEmpty(Ime))
            {
                neobrisani = neobrisani.Where(s => s.Ime.ToLower().Contains(Ime.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(Prezime))
            {
                neobrisani = neobrisani.Where(s => s.Prezime.ToLower().Contains(Prezime.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(Uloga))
            {
                neobrisani = neobrisani.Where(s => s.Uloga.ToString().ToLower().Contains(Uloga.ToLower())).ToList();
            }

            HttpContext.Application["korisnici"] = neobrisani;
            return RedirectToAction("Svi_Administrator", "Administrator");
        }

        public ActionResult Smestaji(string Naziv, string Tip, string Spa, string Wifi, string Prilagodjeno, string Bazen)
        {
            List<Model_Smestaj> smestaji = Models.Repository.Ucitaj_Smestaj();
            List<Model_Smestaj> bag = new List<Model_Smestaj>();
            foreach (Model_Smestaj s in smestaji)
            {
                if (s.Obrisan == false)
                    bag.Add(s);
            }
            if (!string.IsNullOrEmpty(Wifi))
            {
                bool wifi = Wifi == "Da" ? true : false;
                bag = bag.Where(s => s.Wifi == wifi).ToList();
            }
            if (!string.IsNullOrEmpty(Naziv))
                bag = bag.Where(s => s.Naziv.ToLower().Contains(Naziv.ToLower())).ToList();

            if (!string.IsNullOrEmpty(Tip))
                bag = bag.Where(s => s.Tip.ToString().ToLower().Contains(Tip.ToLower())).ToList();
            if (!string.IsNullOrEmpty(Bazen))
            {
                bool bazen = Bazen == "Da" ? true : false;
                bag = bag.Where(s => s.Bazen == bazen).ToList();
            }
            if (!string.IsNullOrEmpty(Prilagodjeno))
            {
                bool prilagodjeno = Prilagodjeno == "Da" ? true : false;
                bag = bag.Where(s => s.Prilagodjeno == prilagodjeno).ToList();
            }
            if (!string.IsNullOrEmpty(Spa))
            {
                bool spa = Spa == "Da" ? true : false;
                bag = bag.Where(s => s.Spa == spa).ToList();
            }

            HttpContext.Application["smestaji_advanced"] = bag;
            return RedirectToAction("SmestajiAdvanced_Menadzer", "Menadzer");
        }

    }
}
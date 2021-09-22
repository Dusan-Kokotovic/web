using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_projekat.Models;

namespace web_projekat.Controllers
{
    public class MenadzerController : Controller
    {
        // GET: Menadzer
        public ActionResult Index_Menadzer()
        {
            ViewBag.prikaz = (List<Model_Aranzman>)HttpContext.Application["aranzmani"];
            return View();
        }
        public ActionResult Tekuci_Menadzer()
        {
            ViewBag.prikaz = (List<Model_Aranzman>)HttpContext.Application["aranzmani"];
            return View();
        }
        public ActionResult Detalji_Menadzer()
        {
            ViewBag.aranzman = (Model_Aranzman)HttpContext.Application["aranzman"];
            return View();
        }

        public ActionResult Smestaj_Menadzer()
        {
            ViewBag.smestaj = (ViewModel)HttpContext.Application["smestaj"];
            return View();
        }
        public ActionResult Jedinice_Menadzer()
        {
            ViewBag.jedinice = (List<Model_Jedinica>)HttpContext.Application["jedinice"];
            return View();
        }
        public ActionResult Utisci_Menadzer()
        {
            ViewBag.utisci = (List<Model_Komentar>)HttpContext.Application["utisci"];
            return View();
        }
        public ActionResult KomentariNaCekanju_Menadzer()
        {
            ViewBag.komentari = (List<Model_Komentar>)HttpContext.Application["komentari_na_cekanju"];
            return View();
        }
        public ActionResult RezervacijeMojih_Menadzer()
        {
            ViewBag.rezervacije = (List<Model_Rezervacija>)HttpContext.Application["rezervacije_mojih"];
            return View();
        }
        public ActionResult PrikazRezervacije_Menadzer()
        {
            Model_Rezervacija rezervacija = (Model_Rezervacija)HttpContext.Application["prikaz_rezervacija"];
            ViewBag.rezervacija = rezervacija;
            return View();
        }
        public ActionResult Aranzmani_Menadzer()
        {
            ViewBag.aranzmani = (List<Model_Aranzman>)HttpContext.Application["menadzer_aranzmani"];
            return View();
        }
        public ActionResult DetaljiMenadzerov_Menadzer()
        {
            ViewBag.aranzman = (Model_Aranzman)HttpContext.Application["aranzman_menadzerov"];
            return View();
        }
        public ActionResult Izmena_Aranzmana_Menadzer()
        {
            ViewBag.aranzman = (Model_Aranzman)HttpContext.Application["aranzman_za_izmenu"];
            return View();
        }
        public ActionResult SmestajiAdvanced_Menadzer()
        {
            ViewBag.smestaji = (List<Model_Smestaj>)HttpContext.Application["smestaji_advanced"];
            return View();
        }
        public ActionResult IzmenaSmestaja_Menadzer()
        {
            ViewBag.smestaj = (Model_Smestaj)HttpContext.Application["smestaj_za_izmenu"];
            return View();
        }
        public ActionResult JediniceAdvanced_Menadzer()
        {
            ViewBag.jedinice = (List<Model_Jedinica>)HttpContext.Application["jedinice_za_prikaz"];
            return View();
        }
        public ActionResult IzmenaJediniceMenadzer()
        {
            ViewBag.jedinica = (Model_Jedinica)HttpContext.Application["jedinica_za_izmenu"];
            return View();
        }
    }
}
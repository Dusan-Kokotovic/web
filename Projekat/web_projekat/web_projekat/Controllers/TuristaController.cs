using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_projekat.Models;

namespace web_projekat.Controllers
{
    public class TuristaController : Controller
    {
        // GET: Turista
        public ActionResult Index_Turista()
        {
            ViewBag.prikaz = (List<Model_Aranzman>)HttpContext.Application["aranzmani"];
            return View();
        }
        public ActionResult Tekuci_Turista()
        {
            ViewBag.prikaz = (List<Model_Aranzman>)HttpContext.Application["aranzmani"];
            return View();
        }
        public ActionResult Detalji_Turista()
        {
            ViewBag.aranzman = (Model_Aranzman)HttpContext.Application["aranzman"];
            return View();
        }

        public ActionResult Smestaj_Turista()
        {
            ViewBag.smestaj = (ViewModel)HttpContext.Application["smestaj"];
            return View();
        }
        public ActionResult Jedinice_Turista()
        {
            ViewBag.jedinice = (List<Model_Jedinica>)HttpContext.Application["jedinice"];
            return View();
        }
        public ActionResult Utisci_Turista()
        {
            ViewBag.utisci = (List<Model_Komentar>)HttpContext.Application["utisci"];
            return View();
        }
        public ActionResult Rezervacije_Turista()
        {
            ViewBag.rezervacije = (List<Model_Rezervacija>)HttpContext.Application["rezervacije"];
            return View();
        }
        public ActionResult DodajKomentar_Turista()
        {
            ViewBag.protekli = (List<Model_Rezervacija>)HttpContext.Application["protekli"];
            return View();
        }
    }
}
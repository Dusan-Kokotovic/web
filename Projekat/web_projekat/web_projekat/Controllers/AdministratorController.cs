using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_projekat.Models;

namespace web_projekat.Controllers
{
    public class AdministratorController : Controller
    {
        public ActionResult Index_Administrator()
        {
            ViewBag.prikaz = (List<Model_Aranzman>)HttpContext.Application["aranzmani"];
            return View();
        }
        public ActionResult Tekuci_Administrator()
        {
            ViewBag.prikaz = (List<Model_Aranzman>)HttpContext.Application["aranzmani"];
            return View();
        }
        public ActionResult Detalji_Administrator()
        {
            ViewBag.aranzman = (Model_Aranzman)HttpContext.Application["aranzman"];
            return View();
        }

        public ActionResult Smestaj_Administrator()
        {
            ViewBag.smestaj = (ViewModel)HttpContext.Application["smestaj"];
            return View();
        }
        public ActionResult Jedinice_Administrator()
        {
            ViewBag.jedinice = (List<Model_Jedinica>)HttpContext.Application["jedinice"];
            return View();
        }
        public ActionResult Utisci_Administrator()
        {
            ViewBag.utisci = (List<Model_Komentar>)HttpContext.Application["utisci"];
            return View();
        }
        public ActionResult Svi_Administrator()
        {
            ViewBag.korisnici = (List<Model_Korisnik>)HttpContext.Application["korisnici"];
            return View();
        }
        public ActionResult Sumnjivi_Administrator()
        {
            ViewBag.sumnjivi = (List<Model_Korisnik>)HttpContext.Application["sumnjivi"];
            return View();
        }
    }
}
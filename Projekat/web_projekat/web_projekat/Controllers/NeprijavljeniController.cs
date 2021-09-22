using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_projekat.Models;

namespace web_projekat.Controllers
{
    public class NeprijavljeniController : Controller
    {
        // GET: Sort
        public ActionResult Index_Neprijavljeni()
        {
            ViewBag.prikaz = (List<Model_Aranzman>)HttpContext.Application["aranzmani"];
            return View();
        }
        public ActionResult Tekuci_Neprijavljeni()
        {
            ViewBag.prikaz = (List<Model_Aranzman>)HttpContext.Application["aranzmani"];
            return View();
        }
        public ActionResult Detalji_Neprijavljeni()
        {
            ViewBag.aranzman = (Model_Aranzman)HttpContext.Application["aranzman"];
            return View();
        }

        public ActionResult Smestaj_Neprijavljeni()
        {
            ViewBag.smestaj = (ViewModel)HttpContext.Application["smestaj"];
            return View();
        }
        public ActionResult Jedinice_Neprijavljeni()
        {
            ViewBag.jedinice = (List<Model_Jedinica>)HttpContext.Application["jedinice"];
            return View();
        }
        public ActionResult Utisci_Neprijavljeni()
        {
            ViewBag.utisci = (List<Model_Komentar>)HttpContext.Application["utisci"];
            return View();
        }
    }
}
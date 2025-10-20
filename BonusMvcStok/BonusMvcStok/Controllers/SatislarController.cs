using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonusMvcStok.Models.Entity;

namespace BonusMvcStok.Controllers
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var satislar = db.tblsatislar.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            //Urunler
            List<SelectListItem> urun = (from x in db.tblurunler.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString()
                                         }).ToList();
            ViewBag.drop1 = urun;
            //urunfiyat
            List<SelectListItem> urunfiyat = (from x in db.tblurunler.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.satisfiyat.ToString(),
                                             Value = x.id.ToString(),
                                         }).ToList();
            ViewBag.drop4 = urunfiyat;
            //Personel
            List<SelectListItem> per = (from x in db.tblpersonel.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad + " " + x.soyad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop2 = per;
            //Musteriler
            List<SelectListItem> must = (from x in db.tblmusteri.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad + " " + x.soyad,
                                             Value = x.id.ToString()
                                         }).ToList();
            ViewBag.drop3 = must;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(tblsatislar p)
        {

            var urun = db.tblurunler.Where(x => x.id == p.tblurunler.id).FirstOrDefault();
            var musteri = db.tblmusteri.Where(x => x.id == p.tblmusteri.id).FirstOrDefault();
            var personel = db.tblpersonel.Where(x => x.id == p.tblpersonel.id).FirstOrDefault();
            p.tblurunler = urun;
            p.tblmusteri = musteri;
            p.tblpersonel = personel;
            p.tarih =DateTime.Parse( DateTime.Now.ToShortDateString());
            db.tblsatislar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
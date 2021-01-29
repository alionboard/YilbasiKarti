using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using YilbasiKarti.Models;

namespace YilbasiKarti.Controllers
{
    public class HomeController : Controller
    {
        UygulamaDbContext db = new UygulamaDbContext();
        public ActionResult Index()
        {

            return View(db.Kartlar.OrderByDescending(x=>x.Id).ToList());
        }

        public ActionResult Yeni()
        {
            return View(new KartResim { Resim = Directory.EnumerateFiles(Server.MapPath("~/Content/images")) });  //klasördeki dosyaları string dizisinde tutuyor.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Yeni(Kart kart)
        {
            if (ModelState.IsValid)
            {
                db.Kartlar.Add(new Kart { Baslik = kart.Baslik, GonderenAd = kart.GonderenAd, AliciAd = kart.AliciAd, Mesaj = kart.Mesaj, ResimAd = kart.ResimAd });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(new KartResim { Kart = kart, Resim = Directory.EnumerateFiles(Server.MapPath("~/Content/images")) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sil(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Kart kart = db.Kartlar.Find(id);
            if (kart == null)
                return HttpNotFound();

            db.Kartlar.Remove(kart);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Duzenle(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Kart kart = db.Kartlar.Find(id);
            if (kart == null)
                return HttpNotFound();

            return View(new KartResim { Kart = kart, Resim = Directory.EnumerateFiles(Server.MapPath("~/Content/images")) });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(Kart kart)
        {
            if (ModelState.IsValid)
            {
                Kart duzenlenecekKart = db.Kartlar.Find(kart.Id);
                duzenlenecekKart.Baslik = kart.Baslik;
                duzenlenecekKart.GonderenAd = kart.GonderenAd;
                duzenlenecekKart.AliciAd = kart.AliciAd;
                duzenlenecekKart.Mesaj = kart.Mesaj;
                duzenlenecekKart.ResimAd = kart.ResimAd;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(new KartResim { Kart = kart, Resim = Directory.EnumerateFiles(Server.MapPath("~/Content/images")) });
        }

        public ActionResult Goruntule(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Kart kart = db.Kartlar.Find(id);
            if (kart == null)
                return HttpNotFound();

            return View(kart);
        }


        public ActionResult Indir(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Kart kart = db.Kartlar.Find(id);
            if (kart == null)
                return HttpNotFound();


            string kartKonum = string.Format(@"C:\Users\ali\source\repos\YilbasiKarti\YilbasiKarti\Content\images\{0}", kart.ResimAd);
            Bitmap bmp = new Bitmap(340, 380);
            Bitmap img = new Bitmap(kartKonum);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height); //340,380
            g.DrawRectangle(Pens.Black, 5, 5, 330, 370);
            g.DrawImage(img, 30, 7, 280, 260);
            g.DrawString(kart.Baslik, new Font("arial", 14f), Brushes.Black, 90, 250);

            string aliciAd = kart.AliciAd;
            if (aliciAd.Length>13)
            {
                g.DrawString(kart.AliciAd, new Font("arial", 14f), Brushes.Black,40, 272);

            }
            else
            {
                g.DrawString(kart.AliciAd, new Font("arial", 14f), Brushes.Black, 100, 272);

            }


            string mesaj = kart.Mesaj;
            if (mesaj.Length > 40 && mesaj.Length < 80)
            {
                g.DrawString(mesaj.Substring(0, 40), new Font("arial", 12f), Brushes.Black, 30, 295);
                g.DrawString(mesaj.Substring(40, (mesaj.Length - 41)), new Font("arial", 12f), Brushes.Black, 30, 312);

            }
            else if (mesaj.Length > 40)
            {
                g.DrawString(mesaj.Substring(0, 33), new Font("arial", 12f), Brushes.Black, 30, 295);
                g.DrawString(mesaj.Substring(33, 33), new Font("arial", 12f), Brushes.Black, 30, 312);
                g.DrawString(mesaj.Substring(66, (mesaj.Length - 81)), new Font("arial", 12f), Brushes.Black, 30, 331);
            }
            else
            {
                g.DrawString(mesaj, new Font("arial", 12f), Brushes.Black, 30, 295);

            }
            string gonderenAd = kart.GonderenAd;

            if (gonderenAd.Length < 13)
            {
                g.DrawString("-" + kart.GonderenAd, new Font("arial", 12f), Brushes.Black, 230, 350);
            }
            else
            {
                g.DrawString("-"+kart.GonderenAd, new Font("arial", 12f), Brushes.Black, 120 , 350);

            }




            byte[] data;
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Png);
                data = ms.ToArray();
            }

            // FileContentResult
            return File(data, MediaTypeNames.Image.Jpeg);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
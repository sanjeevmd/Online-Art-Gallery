using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using P_Art.Models;

namespace P_Art.Controllers
{
    public class ArtSalesController : Controller
    {
        private ArtGalleryEntities db = new ArtGalleryEntities();

        // GET: ArtSales
        public ActionResult Index()
        {
            var artSales = db.ArtSales.Include(a => a.Bid);
            return View(artSales.ToList());
        }

        // GET: ArtSales/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtSale artSale = db.ArtSales.Find(id);
            if (artSale == null)
            {
                return HttpNotFound();
            }
            return View(artSale);
        }

        // GET: ArtSales/Create
        public ActionResult Create()
        {
            ViewBag.BidID = new SelectList(db.Bids, "BidID", "BidID");
            ViewBag.date = DateTime.Now.ToString();
            return View();
        }

        public ViewResult Details()
        {
            throw new NotImplementedException();
        }

        // POST: ArtSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleID,SoldDate,DeliveryAddress,BidID,PaymentStatus")] ArtSale artSale)
        {
            if (ModelState.IsValid)
            {
                db.ArtSales.Add(artSale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BidID = new SelectList(db.Bids, "BidID", "BidID", artSale.BidID);
            return View(artSale);
        }

        // GET: ArtSales/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtSale artSale = db.ArtSales.Find(id);
            if (artSale == null)
            {
                return HttpNotFound();
            }
            ViewBag.BidID = new SelectList(db.Bids, "BidID", "BidID", artSale.BidID);
            return View(artSale);
        }

        // POST: ArtSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleID,SoldDate,DeliveryAddress,BidID,PaymentStatus")] ArtSale artSale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artSale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BidID = new SelectList(db.Bids, "BidID", "BidID", artSale.BidID);
            return View(artSale);
        }

        // GET: ArtSales/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtSale artSale = db.ArtSales.Find(id);
            if (artSale == null)
            {
                return HttpNotFound();
            }
            return View(artSale);
        }

        // POST: ArtSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ArtSale artSale = db.ArtSales.Find(id);
            db.ArtSales.Remove(artSale);
            db.SaveChanges();
            return RedirectToAction("Index");
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

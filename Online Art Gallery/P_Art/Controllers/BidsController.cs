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
    public class BidsController : Controller
    {
        private ArtGalleryEntities db = new ArtGalleryEntities();

        // GET: Bids
        public ActionResult Index()
        {
            var bids = db.Bids.Include(b => b.ArtWork).Include(b => b.UserInformation);
            return View(bids.ToList());
        }

        // GET: Bids/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // GET: Bids/Create
        public ActionResult Create()
        {
            ViewBag.user = User.Identity.Name;
            ViewBag.ArtID = new SelectList(db.ArtWorks, "ArtID", "ArtTitle");
            ViewBag.ArtCollectorID = new SelectList(db.UserInformations, "UserID", "FirstName");
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BidID,ArtID,ArtCollectorID,Bidamount,IsAllocated")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                db.Bids.Add(bid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtID = new SelectList(db.ArtWorks, "ArtID", "ArtTitle", bid.ArtID);
            ViewBag.ArtCollectorID = new SelectList(db.UserInformations, "UserID", "FirstName", bid.ArtCollectorID);
            return View(bid);
        }

        // GET: Bids/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtID = new SelectList(db.ArtWorks, "ArtID", "ArtTitle", bid.ArtID);
            ViewBag.ArtCollectorID = new SelectList(db.UserInformations, "UserID", "FirstName", bid.ArtCollectorID);
            return View(bid);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BidID,ArtID,ArtCollectorID,Bidamount,IsAllocated")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtID = new SelectList(db.ArtWorks, "ArtID", "ArtTitle", bid.ArtID);
            ViewBag.ArtCollectorID = new SelectList(db.UserInformations, "UserID", "FirstName", bid.ArtCollectorID);
            return View(bid);
        }

        // GET: Bids/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Bid bid = db.Bids.Find(id);
            db.Bids.Remove(bid);
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

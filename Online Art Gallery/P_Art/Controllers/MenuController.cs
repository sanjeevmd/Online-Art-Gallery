using P_Art.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace P_Art.Controllers
{
    public class MenuController : Controller
    {
        private ArtGalleryEntities db = new ArtGalleryEntities();
        [Authorize(Roles ="AD")]
        // GET: Menu
        public ActionResult Admin()
        {
            return View();
        }
        [Authorize(Roles ="AR")]
        public ActionResult Artist()
        {
            return View();
        }
        [Authorize(Roles ="CO")]
        public ActionResult Collector()
        {
            return View();
        }
        public ActionResult Approve()
        {
            return View();
        }
        public ActionResult collectorid()
        {
            return View();
        }
        [Authorize(Roles = "AR")]
        public ActionResult Modify()
        {
            var artworks = db.ArtModify(int.Parse(User.Identity.Name));
            TempData["id"] = int.Parse(User.Identity.Name);
            //var artworks = db.artworks.Include(a => a.userinformation).Include(a => a.category);
            return View(artworks.ToList());
        }
        [Authorize(Roles ="AR")]
        public ActionResult Edit(long? id)
        {
            ViewBag.user = User.Identity.Name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtWork artWork = db.ArtWorks.Find(id);
            if (artWork == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistID = new SelectList(db.UserInformations, "UserID", "FirstName", artWork.ArtistID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", artWork.CategoryID);
            return View(artWork);
        }

        // POST: ArtWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtID,ArtTitle,ArtDescription,ArtistID,CategoryID,ImageURL,PostedDate,MinimumBidAmount,CurrentBidAmount,IsApproved,IsSold")] ArtWork artWork)
        {
            artWork.ArtistID = int.Parse(User.Identity.Name);
           // artWork.CategoryID = 3;
            db.ArtWorks.Add(artWork);
            if (ModelState.IsValid)
            {
                
                db.Entry(artWork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Modify");
            }
            ViewBag.ArtistID = new SelectList(db.UserInformations, "UserID", "FirstName", artWork.ArtistID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", artWork.CategoryID);
            return View(artWork);
        }

        // GET: ArtWorks/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtWork artWork = db.ArtWorks.Find(id);
            if (artWork == null)
            {
                return HttpNotFound();
            }
            return View(artWork);
        }

        // POST: ArtWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ArtWork artWork = db.ArtWorks.Find(id);
            db.ArtWorks.Remove(artWork);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult SoldUnsold()
        {
            var artworks = db.ArtSoldUnsold(int.Parse(User.Identity.Name));
            //var artworks = db.artworks.Include(a => a.userinformation).Include(a => a.category);
            return View(artworks.ToList());
        }
        public ActionResult HighestBidder(int? id)
        {
            var artworks = db.HighestBid(int.Parse(User.Identity.Name),id);
            //var artworks = db.artworks.Include(a => a.userinformation).Include(a => a.category);
            return View(artworks.ToList());
        }
        public ActionResult BidStatus()
        {
            var artworks = db.BidStatus(int.Parse(User.Identity.Name));
            //var artworks = db.artworks.Include(a => a.userinformation).Include(a => a.category);
            return View(artworks.ToList());
        }
        public ActionResult SaleIDlast(int?id)
        {
            var art = db.saleidlast(id);
            return View(art.ToList());
        }
        public ActionResult Cats()
        {
          //  var artworks = db.BidStatus(int.Parse(User.Identity.Name));
            return View(db.Categories.ToList());
        }
        public ActionResult Detailscat(int? id)
        {
            var artworks = db.categoryview(id);
            //var artworks = db.artworks.Include(a => a.userinformation).Include(a => a.category);
            return View(artworks.ToList());
        }
        public ActionResult Detailscat1(int? id)
        {
            var artworks = db.categoryview1(id);
            //var artworks = db.artworks.Include(a => a.userinformation).Include(a => a.category);
            return View(artworks.ToList());
        }
        public ActionResult Createbid(long? id)
        {
            ViewBag.id = id;
            TempData["ID"] = id;
            ViewBag.user = User.Identity.Name;
            ViewBag.ArtID = new SelectList(db.ArtWorks, "ArtID", "ArtTitle");
            ViewBag.ArtCollectorID = new SelectList(db.UserInformations, "UserID", "FirstName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createbid([Bind(Include = "BidID,ArtID,ArtCollectorID,Bidamount,IsAllocated")] Bid bid)
        {
            bid.ArtCollectorID = int.Parse(User.Identity.Name);
            bid.ArtID = Convert.ToInt32(TempData["ID"]);
            if (ModelState.IsValid)
            {
                db.Bids.Add(bid);
                db.SaveChanges();
                return RedirectToAction("Collector");
            }

            ViewBag.ArtID = new SelectList(db.ArtWorks, "ArtID", "ArtTitle", bid.ArtID);
            ViewBag.ArtCollectorID = new SelectList(db.UserInformations, "UserID", "FirstName", bid.ArtCollectorID);
            return View(bid);
        }

    }
}
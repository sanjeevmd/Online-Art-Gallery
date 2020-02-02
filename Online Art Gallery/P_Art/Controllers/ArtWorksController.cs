using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using P_Art.Models;
using System.IO;

namespace P_Art.Controllers
{
    public class ArtWorksController : Controller
    {
        private ArtGalleryEntities db = new ArtGalleryEntities();

        // GET: ArtWorks
        public ActionResult Index()
        {
            var artWorks = db.ArtWorks.Include(a => a.UserInformation).Include(a => a.Category);
            return View(artWorks.ToList());
        }

        // GET: ArtWorks/Details/5
        public ActionResult Details(long? id)
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

        // GET: ArtWorks/Create
        public ActionResult Create()
        {
            ViewBag.ArtistID = new SelectList(db.UserInformations, "UserID", "FirstName");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.user = User.Identity.Name;
            ViewBag.date = DateTime.Now.ToString();
            return View();
        }

        // POST: ArtWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtID,ArtTitle,ArtDescription,ArtistID,CategoryID,ImageURL,PostedDate,MinimumBidAmount")] ArtWork artWork, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
               if (file != null)
                {
                    string ImageName = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath = Path.Combine(Server.MapPath("~/Images"), ImageName);
                    file.SaveAs(physicalPath);
                    artWork.ImageURL = Url.Content(@"~/images/" + ImageName);
                }
                artWork.ArtistID =int.Parse(User.Identity.Name);                
                db.ArtWorks.Add(artWork);
                db.SaveChanges();
                return RedirectToAction("Artist","Menu");
            }

            ViewBag.ArtistID = new SelectList(db.UserInformations, "UserID", "FirstName", artWork.ArtistID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", artWork.CategoryID);
            return View("Artist","Menu");
        }

        // GET: ArtWorks/Edit/5
        public ActionResult Edit(long? id)
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
            if (ModelState.IsValid)
            {
                db.Entry(artWork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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

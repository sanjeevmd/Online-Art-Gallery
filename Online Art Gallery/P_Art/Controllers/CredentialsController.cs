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
    public class CredentialsController : Controller
    {
        private ArtGalleryEntities db = new ArtGalleryEntities();
        [Authorize(Roles = "AD")]
        // GET: Credentials
        public ActionResult Index()
        {
            var credentials = db.Credentials.Include(c => c.UserInformation);
            return View(credentials);
        }
        [Authorize(Roles = "AD")]
        // GET: Credentials/Details/5
        public ActionResult Details(int? id)
        {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credential credential = db.Credentials.Find(id);
            if (credential == null)
            {
                return HttpNotFound();
            }
            return View(credential);
        }
        [Authorize(Roles = "AD")]
        // GET: Credentials/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.UserInformations,"UserID","FirstName");
            return View();
        }
        [Authorize(Roles = "AD")]
        // POST: Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Password,UserType")] Credential credential)
        {
            if (ModelState.IsValid)
            {
                db.Credentials.Add(credential);
                db.SaveChanges(); 
                return RedirectToAction("Index","Home");
            }

            ViewBag.UserID = new SelectList(db.UserInformations, "UserID", "FirstName", credential.UserID);
            return View(credential);
        }
        [Authorize(Roles = "AD")]
        // GET: Credentials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credential credential = db.Credentials.Find(id);
            if (credential == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.UserInformations, "UserID", "FirstName", credential.UserID);
            return View(credential);
        }
        [Authorize(Roles = "AD")]
        // POST: Credentials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Password,UserType")] Credential credential)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credential).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.UserInformations, "UserID", "FirstName", credential.UserID);
            return View(credential);
        }
        [Authorize(Roles = "AD")]
        // GET: Credentials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credential credential = db.Credentials.Find(id);
            if (credential == null)
            {
                return HttpNotFound();
            }
            return View(credential);
        }
        [Authorize(Roles = "AD")]
        // POST: Credentials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Credential credential = db.Credentials.Find(id);
            db.Credentials.Remove(credential);
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

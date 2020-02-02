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
    public class UserInformationsController : Controller
    {
        private ArtGalleryEntities db = new ArtGalleryEntities();
        // GET: UserInformations
        [Authorize(Roles ="AD")]
        public ActionResult Index()
        {
            var userInformations = db.UserInformations.Include(u => u.Credential);
            return View(userInformations.ToList());
        }
        [Authorize(Roles = "AD")]
        // GET: UserInformations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInformation userInformation = db.UserInformations.Find(id);
            if (userInformation == null)
            {
                return HttpNotFound();
            }
            return View(userInformation);
        }
        // GET: UserInformations/Create
        public ActionResult Create()
        {
            ViewBag.message = "Register Here";
            ViewBag.UserID = new SelectList(db.Credentials, "UserID", "Password");
            return View();
        }

        // POST: UserInformations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,LastName,Gender,DateOfBirth,Email,Phone,HouseNumber,Street,City,State,Country,PinCode,Password,UserType")] UserInformation userInformation,Credential credential)
        {
            if (ModelState.IsValid)
            {
                db.UserInformations.Add(userInformation);
                db.Credentials.Add(credential);
                db.SaveChanges();
                TempData["User"] = userInformation.UserID;
                return RedirectToAction("Submit","Home");
            }

            ViewBag.UserID = new SelectList(db.Credentials, "UserID", "Password", userInformation.UserID);
            return View(userInformation);
        }
        [Authorize(Roles = "AD")]
        // GET: UserInformations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInformation userInformation = db.UserInformations.Find(id);
            if (userInformation == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Credentials, "UserID", "Password", userInformation.UserID);
            return View(userInformation);
        }
        [Authorize(Roles = "AD")]
        // POST: UserInformations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,LastName,Gender,DateOfBirth,Email,Phone,HouseNumber,Street,City,State,Country,PinCode")] UserInformation userInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Credentials, "UserID", "Password", userInformation.UserID);
            return View(userInformation);
        }
        [Authorize(Roles = "AD")]
        // GET: UserInformations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInformation userInformation = db.UserInformations.Find(id);
            if (userInformation == null)
            {
                return HttpNotFound();
            }
            return View(userInformation);
        }
        [Authorize(Roles = "AD")]
        // POST: UserInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInformation userInformation = db.UserInformations.Find(id);
            db.UserInformations.Remove(userInformation);
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

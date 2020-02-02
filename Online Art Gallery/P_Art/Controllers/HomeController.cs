using P_Art.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace P_Art.Controllers
{ 
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.message = "WELCOME TO ONLINE ART GALLERY";
            ViewBag.para = "Art Gallery is basically the display of creative artistic works of one artist or a collective group of artistsArt exhibition is basically the display of creative artistic works of one artist or a collective group of artists.";
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.message = "Your are at the right place";
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(P_Art.Models.Credential user )
        {
            using (ArtGalleryEntities db = new ArtGalleryEntities())
            {
                var userdetails = db.Credentials.Where(x => x.UserID == user.UserID && x.Password == user.Password).Count(); 
                if (userdetails > 0)
                {
                    FormsAuthentication.RedirectFromLoginPage(user.UserID.ToString(), true);
                    return RedirectToAction("Index");
                }
                else
                {
                    user.Loginerrormessage = "Wrong UserName or Password";
                    return View("Login",user);
                }
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        //public ActionResult Followus()
        //{
        //    ViewBag.Message = "Your Login pag";
        //    return View();
        //}
        public ActionResult About()
        {
            ViewBag.message = "Know us Better ! ";
            ViewBag.para = "Art Gallery is basically the display of creative artistic works of one artist or a collective group of artistsArt exhibition is basically the display of creative artistic works of one artist or a collective group of artists.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.message = "Feel Free to Contact Us !";

            return View();
        }
        public ActionResult Submit()
        {
            UserInformation userinformation = TempData["User"] as UserInformation;
            ViewBag.message = "Registration Successful ! Please Note Your User ID for Login ";
            ViewBag.para = TempData["User"];
            return View();
        }
    }
}
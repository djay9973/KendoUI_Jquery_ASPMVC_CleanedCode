using KendoUI_Jquery_ASPMVC.Services.Interface;
using KendoUI_Jquery_ASPMVC.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoUI_Jquery_ASPMVC.Models;
using System.Data;
using System.IO;
using System.Security.Principal;
using KendoUI_Jquery_ASPMVC.ViewModel;
using System.Web.Security;

namespace KendoUI_Jquery_ASPMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Account1Controller : Controller
    {
        private readonly BooksEntities db = new BooksEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                bool IsValidUser = db.Users.Any(user => user.Username.ToLower() ==
                userModel.UserName.ToLower() && user.Password == userModel.UserPassword);

                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(userModel.UserName, false);
                    return RedirectToAction("Index", "Help");
                }
                ModelState.AddModelError("", "Invalid username and password");
            }
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
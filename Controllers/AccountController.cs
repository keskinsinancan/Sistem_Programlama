using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SysProgAnket3.Models;

namespace SysProgAnket3.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        private readonly SysProgDbEntities db = new SysProgDbEntities();
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "AccessDenied");
            }

            return View("~/Views/Account/Login.cshtml");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var user = db.users.Where(m => m.username.Equals(model.username) && m.password.Equals(model.password)).FirstOrDefault();
            //var obj = db.UserProfiles.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.username, false);
                return RedirectToAction("Index", "Lecture");
            }
            else
            {
                ViewBag.message = "Geçersiz kullanıcı adı veya şifre..";
                return View("~/Views/Account/Login.cshtml");
            }
        }

        public ActionResult Logout ()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}
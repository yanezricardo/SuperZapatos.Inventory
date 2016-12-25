using Inventory.Site.Models;
using Inventory.Site.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Inventory.Site.Controllers {
    public class AccountController : Controller {
        public ActionResult Login() {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login) {
            if (!ModelState.IsValid) {
                ViewBag.Error = HttpStatusCode.BadRequest.ToString();
                return View("Login");
            }
            if (login.UserName == "my_user" && login.Password == "my_password") {
                FormsAuthentication.RedirectFromLoginPage(login.UserName, true);

                var authentication = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", login.UserName, login.Password)));
                var ticket = new FormsAuthenticationTicket(1, login.UserName, DateTime.Now, DateTime.Now.AddHours(3), false, authentication);
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) {                    
                    HttpOnly = true,
                    Secure = Request.IsSecureConnection,
                };
                Response.Cookies.Set(cookie);
            }
            ViewBag.Error = "Usuario o clave inválidos";
            return View("Login");
        }

        public ActionResult LogOff() {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}
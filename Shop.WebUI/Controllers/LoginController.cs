using Shop.Core.Models;
using Shop.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUI.Controllers
{
    public class LoginController : Controller
    {
        SQLRepository<User> context;
        public LoginController()
        {
            context = new SQLRepository<User>(new MyContext());
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            string msgError = "Echec authentification";
            if (ModelState.IsValid)
            {
                User dbUser = context.Collection().SingleOrDefault(user => user.Email.Equals(u.Email));

                if (dbUser != null)
                {
                    if (dbUser.Password.Equals(u.Password))
                    {
                        Session["user_id"] = dbUser.Id;

                        return RedirectToAction("Index", "ProductManager");
                    }
                    else
                    {
                        ViewBag.error = msgError;
                    }
                }
                else
                {
                    ViewBag.error = msgError;
                }
            }
            else
            {
                ViewBag.error = msgError;
            }
            return View("Index", u);
        }

        public ActionResult Logout()
        {
            Session.Remove("user_id"); //Vider le contenu de la session 
            
            return View("Index", new User());
        }
    }
}
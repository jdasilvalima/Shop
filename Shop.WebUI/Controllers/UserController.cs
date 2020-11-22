using Shop.Core.Models;
using Shop.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUI.Controllers
{
    public class UserController : Controller
    {
        SQLRepository<User> context;

        public UserController()
        {
            context = new SQLRepository<User>(new MyContext());
        }

        // GET: User
        public ActionResult Index()
        {
            List<User> users = context.Collection().ToList();
            return View(users);
        }

        public ActionResult Create()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User u)
        {
            if (!ModelState.IsValid)
            {
                return View(u);
            }
            else
            {
                context.Insert(u);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(int id)
        {
            try
            {
                User u = context.FindById(id);
                if (u == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(u);
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User u, int id)
        {

            if (!ModelState.IsValid)
            {
                return View(u);
            }
            else
            {
                context.Update(u);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                User u = context.FindById(id);
                if (u == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(u);
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                User uToDelete = context.FindById(id);
                if (uToDelete == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    context.Delete(id);
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }
        }
    }
}
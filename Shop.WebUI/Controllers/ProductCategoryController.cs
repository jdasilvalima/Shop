using Shop.Core.Models;
using Shop.DataAccess.InMemory;
using Shop.DataAccess.SQL;
using Shop.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUI.Controllers
{
    [LoginFilter]
    public class ProductCategoryController : Controller
    {
        //InMemoryRepository<ProductCategory> context;

        //WithEF - avec DB - final repository
        SQLRepository<ProductCategory> context;

        public ProductCategoryController()
        {
            context = new SQLRepository<ProductCategory>(new MyContext());
        }


        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory pCategory = new ProductCategory();
            return View(pCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            else
            {
                context.Insert(p);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(int id)
        {
            try
            {
                ProductCategory p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory p, int id)
        {
            //ProductCategory catToEdit = context.FindById(id);
            //try
            //{
            //    if (catToEdit == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    else
            //    {
                    if (!ModelState.IsValid)
                    {
                        return View(p);
                    }
                    else
                    {
                        //context.Update(pToEdit);
                        context.Update(p);
                        context.Commit();
                        //catToEdit.Category = p.Category;
                        return RedirectToAction("Index");
                    }
            //    }
            //}
            //catch (Exception)
            //{

            //    return HttpNotFound();
            //}
        }

        public ActionResult Delete(int id)
        {
            try
            {
                ProductCategory p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
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
                ProductCategory pToDelete = context.FindById(id);
                if (pToDelete == null)
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
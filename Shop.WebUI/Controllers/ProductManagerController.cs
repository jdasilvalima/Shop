using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.DataAccess.InMemory;
using Shop.DataAccess.SQL;
using Shop.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUI.Controllers
{
    [LoginFilter]
    public class ProductManagerController : Controller
    {
        //We selected an empty Controller - we also created one by one ProductManager Views

        ////Repository avec cache
        //InMemoryRepository<Product> context;
        ////ProductRepository context;
        //InMemoryRepository<ProductCategory> contextCategory;
        ////ProductCategoryRepository contextCategory;

        //WithEF - avec DB - final repository
        SQLRepository<Product> context;
        SQLRepository<ProductCategory> contextCategory;


        public ProductManagerController()
        {
            //context = new InMemoryRepository<Product>();
            //contextCategory = new InMemoryRepository<ProductCategory>();
            context = new SQLRepository<Product>(new MyContext());
            contextCategory = new SQLRepository<ProductCategory>(new MyContext());
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {


            //Product p = new Product();

            //Créer objet intermédiaire pour envoyer 2 objets à une Vue - car ne peut pas transmettre deux objets à une vue
            //ProductCategoryViewModel >> classe avec plusieurs objets qui permet d'envoyer plusieurs
            ProductCategoryViewModel viewModel = new ProductCategoryViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = contextCategory.Collection();
            return View(viewModel);
        }

        //Par défault le binding ne fonctionne pas à cause la classe intermédiaire > il faut ajouter un intermédiaire
        //soit écrire "Product product" : paramètre avec le même nom que la classe
        //Soit ajouter [Bind(Product p)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("~/Content/ProdImage/") + product.Image);
                }
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Edit(int id)
        {
            try
            {
                Product p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    ProductCategoryViewModel viewModel = new ProductCategoryViewModel();
                    viewModel.Product = p;
                    viewModel.ProductCategories = contextCategory.Collection();
                    return View(viewModel);
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, int id, HttpPostedFileBase file)
        {
            //Product pToEdit = context.FindById(id);
            //try
            //{
            //    if (pToEdit == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    else
            //    {
                    if (!ModelState.IsValid)
                    {
                        //Retourne meme page avec produit non valide
                        return View(product);
                    }
                    else
                    {

                //context.Update(pToEdit); //Pas de BDD donc doit définir les paramètres de facon explicite
                //pToEdit.Name = product.Name;
                //pToEdit.Description = product.Description;
                //pToEdit.Category = product.Category;
                //pToEdit.Price = product.Price;
                //pToEdit.Image = product.Image;


                    if (file != null)
                    {
                        product.Image = product.Id + Path.GetExtension(file.FileName);
                        file.SaveAs(Server.MapPath("~/Content/ProdImage/") + product.Image);
                    }


                    context.Update(product);

                        context.Commit();
                        return RedirectToAction("Index");
                    }
        }
        //}
        //catch (Exception)
        //{

        //    return HttpNotFound();
        //}
    //}

        public ActionResult Delete(int id)
        {
            try
            {
                Product p = context.FindById(id);
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
                Product pToDelete = context.FindById(id);
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
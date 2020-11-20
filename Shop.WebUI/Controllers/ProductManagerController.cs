using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        //We selected an empty Controller - we also created one by one ProductManager Views

        ProductRepository context;
        ProductCategoryRepository contextCategory;

        public ProductManagerController()
        {
            context = new ProductRepository();
            contextCategory = new ProductCategoryRepository();
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
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
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
        public ActionResult Edit(Product product, int id)
        {
            Product pToEdit = context.FindById(id);
            try
            {
                if (pToEdit == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        //Retourne meme page avec produit non valide
                        return View(pToEdit);
                    }
                    else
                    {
                        //context.Update(pToEdit); //Pas de BDD donc doit définir les paramètres de facon explicite
                        pToEdit.Name = product.Name;
                        pToEdit.Description = product.Description;
                        pToEdit.Category = product.Category;
                        pToEdit.Price = product.Price;
                        pToEdit.Image = product.Image;

                        context.Commit();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }
        }

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
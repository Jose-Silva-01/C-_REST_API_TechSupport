using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult ProductsDisplay(string searchProducts, int top = 10, int page = 1)
        {
            int skip = (page - 1) * top;
            var context = new TechSupportEntities();

            List<Product> desiredProducts = context.Products.ToList();
            List<Product> productsDisplay = context.Products.OrderBy(p => p.Name).Skip(skip).Take(top).ToList();

            if (!string.IsNullOrWhiteSpace(searchProducts))
            {
                desiredProducts = desiredProducts.Where(p => p.Name.IndexOf(searchProducts) != -1).ToList();

                productsDisplay = desiredProducts.OrderBy(p => p.Name).Skip(skip).Take(top).ToList();
            }

            
            ViewBag.totalProducts = desiredProducts.Count();
            ViewBag.page = page;
            ViewBag.top = top;
            ViewBag.searchTerm = searchProducts;
            ViewBag.entity = "Products";

            return View(productsDisplay);
        }

        public ActionResult AddProducts(string id = "")
        {
            var context = new TechSupportEntities();

            Product product = context.Products.FirstOrDefault(p => p.ProductCode == id);

            return View(product);
        }

        [HttpPost]
        public ActionResult AddProducts(Product product)
        {
            var context = new TechSupportEntities();
            string message = "";

            try
            {
                if(string.IsNullOrWhiteSpace(product.Name) || string.IsNullOrWhiteSpace(product.ProductCode) || product.Version <= 0)
                {
                    message = "Please provide all the required fields";
                }
                else
                {
                    if (context.Products.Any(p => p.ProductCode == product.ProductCode))
                        message = "The prodcut " + product.Name + " was successfuly updated";
                    else
                        message = "The prodcut " + product.Name + " was successfuly added";

                    context.Products.AddOrUpdate(product);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return new JsonResult()
            {
                Data = new { message },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        public ActionResult DeleteProducts(string txtProductCode = "")
        {
            var context = new TechSupportEntities();

            Product deleteProd = context.Products.FirstOrDefault(p => p.ProductCode == txtProductCode);
            string message = "";

            try
            {
                context.Products.Remove(deleteProd);
                context.SaveChanges();
                message = "Product " + deleteProd.ProductCode.Trim() + " deleted successfuly";
            }
            catch (Exception)
            {

                throw;
            }

            return new JsonResult()
            {
                Data = new { message },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }
    }
}
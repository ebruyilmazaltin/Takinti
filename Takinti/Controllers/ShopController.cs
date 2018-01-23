using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takinti.Models;

namespace Takinti.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public JsonResult AddToCart(string slug)
        {
            using (var db = new ApplicationDbContext())
            {
                if (Session["Cart"] == null)
                {
                    Session["Cart"] = new Cart();
                }

                ((Cart)Session["Cart"]).UserName = User.Identity.Name;
                ((Cart)Session["Cart"]).UpdateDate = DateTime.Now;
                var cartItem = new CartItem();
                cartItem.Quentity = 1;

                var product = db.Products.FirstOrDefault(p => p.Slug.ToLower() == slug.ToLower() 
                && p.IsInStock == true && p.Quantity > 0 && p.IsPublished == true);
                if (product == null)
                {
                    return Json(false); //sepete eklenirken sorun varsa false döner
                }
                cartItem.ProductId = product.Id;
                cartItem.Product = product;
                cartItem.CreateDate = DateTime.Now;

                ((Cart)Session["Cart"]).CartItems.Add(cartItem);
                return Json(CartProductCount()); //sorun yoksa ürün sayısı döner

            }
            
        }
        public int CartProductCount()
        {
            if (Session["Cart"] != null)
            {
                return ((Cart)Session["Cart"]).ProductCount;
            }
            return 0;

        }
    }
}
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store_MVC_Core.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartManager cartManager;
        public CartController(ICartManager cartManager)
        {
            this.cartManager = cartManager;
        }
        public ActionResult GetCart()
        {
            return View();
        }
        //GET: Cart       
        [HttpGet]
        public ViewResult GetCart(Cart cart)
        {
            try
            {
                var result = this.cartManager.GetCart();
                return View(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult AddToCart([FromBody] Cart cart)
        {
            try
            {
                var result = this.cartManager.AddToCart(cart);
                if (result != null)
                {
                    return Json(new { status = true, Message = "Book added to cart", Data = result });
                }
                else
                {
                    return Json(new { status = false, Message = "Book not added to cart", Data = result });
                }
            }
            catch (Exception)
            {
                return ViewBag.Message = "sucessfully";
            }
        }


        [HttpPost]
        public ActionResult RemoveFromCart([FromBody] int cartId)
        {
            try
            {
                var result = this.cartManager.RemoveFromCart(cartId);
                if (result > 0)
                {
                    //return View();
                    return Json(new { status = true, Message = "Book removed from cart", Data = result });
                }
                else
                {
                    return Json(new { status = false, Message = "Failed to Remove", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}

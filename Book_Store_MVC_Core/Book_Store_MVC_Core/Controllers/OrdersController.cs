using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store_MVC_Core.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersManager ordersManager;
        public OrdersController(IOrdersManager ordersManager)
        {
            this.ordersManager = ordersManager;
        }
        //GET: Orders
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout([FromBody] Orders orders)
        {
            try
            {
                var result = this.ordersManager.Checkout(orders);
                if (result != null)
                {
                    return View();

                }
                return ViewBag.Message = "Null";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

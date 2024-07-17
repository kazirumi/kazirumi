using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test10.Data;
using test10.Models;

namespace test10.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(Order anOrder)
        {
            List<products> product = HttpContext.Session.GetObject<List<products>>("cart");
            if (product != null)
            {
                foreach (var item in product)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.productsid = item.producttypeId;
                    orderDetails.productPrice = item.price;
                    orderDetails.productQuantity = item.Quantity;
                    anOrder.OrderDetails.Add(orderDetails);
                }

            }

            anOrder.orderNo = getorderno();
            _context.Order.Add(anOrder);
            await _context.SaveChangesAsync();

            return View(anOrder);
        }
        public string getorderno()
        {
            int rowCount = _context.Order.ToList().Count()+1;
            return rowCount.ToString("000");
        }

        public IActionResult Orders()
        {
            var s = _context.Order.ToList();
            return View(s);
        }
        public IActionResult OrderDetails(int? Id)
        {
            var s = _context.OrderDetails.Include(c=>c.products).Include(c=>c.Order).Where(c=>c.Orderid==Id).ToList();
            return View(s);
        }
    }
}
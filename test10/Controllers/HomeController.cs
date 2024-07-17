using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test10.Models;
using test10.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace test10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var s = _context.products.Include(c => c.producttype).Include(c => c.SpecialTag).ToList();
            return View(s);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult  Privacy()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Details(int Id)
        {
            ViewData["prodtypeid"] = new SelectList(_context.producttype.ToList(), "ID", "prodtype");

            var s = _context.products.Include(c => c.producttype).Include(c => c.SpecialTag).FirstOrDefault(c=>c.Id==Id);

            if (s==null)
            {
                return NotFound();
            }
            return View(s);
        }
        
        
        public IActionResult addcart(int? Id )
        {
            List<products> cartproducts = new List<products>();
            if(Id==null)
            {
                return NotFound();
            }
            var prod = _context.products.Include(c => c.producttype).Include(c => c.SpecialTag).FirstOrDefault(c=>c.Id==Id);
            
            if(prod==null)
            {
                return NotFound();
            }
            cartproducts = HttpContext.Session.GetObject<List<products>>("cart");
            if (cartproducts != null)
            { int fl = 0;
                List<products> newcartproduct = new List<products>();
                foreach (var item in cartproducts)
                {
                    if(item.Id==prod.Id)
                    {
                        fl = 1;
                        item.Quantity = item.Quantity + prod.Quantity;
                        item.price = item.price + prod.price;
                       
                    }
                 
                    
                        newcartproduct.Add(item);
                    
                }
                if (fl == 0)
                {
                    newcartproduct.Add(prod);
                        
                }
                HttpContext.Session.SetObject("cart", newcartproduct);
            }
            else
            {
                cartproducts = new List<products>();
                cartproducts.Add(prod);
                HttpContext.Session.SetObject("cart", cartproducts);
            }
            TempData["addcart"] = "Product has added into Cart";
            return View("Details",prod);
        }

        public IActionResult showcart()
        {
            var v = HttpContext.Session.GetObject<List<products>>("cart");
            if(v==null)
            {
               return NotFound();
            }
            return View(v.ToList());
        }

        public IActionResult RemoveCart(int? id)
        {
            double differ=1;
            var product = _context.products.FirstOrDefault(c=>c.Id==id);
            List<products> oldcart = new List<products>();
            oldcart = HttpContext.Session.GetObject<List<products>>("cart");
            if (oldcart == null)
            {
                return NotFound();
            }
            else
            {
                List<products> newcart = new List<products>();
                foreach (var item in oldcart)
                {
                    if (item.Id == product.Id)
                    {
                        
                        item.Quantity = item.Quantity - product.Quantity;
                        item.price = item.price - product.price;
                        if (item.price == 0)
                        {
                            newcart.Remove(product);    
                        }
                        else
                        {
                            newcart.Add(item);
                        }
                    }
                    else
                    {
                        newcart.Add(item);
                    }
                }
                HttpContext.Session.SetObject("cart",newcart);
            }
            return RedirectToAction(nameof(showcart));
        }

        public IActionResult IncreaseCart(int? Id)
        {
            List<products> cartproducts = new List<products>();
            if (Id == null)
            {
                return NotFound();
            }
            var prod = _context.products.Include(c => c.producttype).Include(c => c.SpecialTag).FirstOrDefault(c => c.Id == Id);

            if (prod == null)
            {
                return NotFound();
            }
            cartproducts = HttpContext.Session.GetObject<List<products>>("cart");
            if (cartproducts != null)
            {
                int fl = 0;
                List<products> newcartproduct = new List<products>();
                foreach (var item in cartproducts)
                {
                    if (item.Id == prod.Id)
                    {
                        fl = 1;
                        item.Quantity = item.Quantity + prod.Quantity;
                        item.price = item.price + prod.price;

                    }


                    newcartproduct.Add(item);

                }
                if (fl == 0)
                {
                    newcartproduct.Add(prod);

                }
                HttpContext.Session.SetObject("cart", newcartproduct);
            }
            return RedirectToAction(nameof(showcart));
        }
        public IActionResult searchProduct(string s)
        {
            if (s != null)
            {
                var r = _context.products.Where(c => c.Name.ToLower().Contains(s.ToLower()) || c.producttype.prodtype.ToLower().Contains(s.ToLower()));
                return Json(r.ToList());
            }
            else
            {
                RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
            
        }
        public IActionResult getproducttype()
        {
            var s = _context.producttype.ToList();
            return Json(s);
        }
        [HttpPost]
        public IActionResult getprodtypebyid(int prodtypeid)
        {
            var s = _context.products.Include(c => c.producttype).Include(c => c.SpecialTag).Where(c=>c.producttype.ID== prodtypeid).ToList();
            return View("Index",s);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

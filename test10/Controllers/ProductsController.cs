using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using test10.Data;
using test10.Models;

namespace test10.Areas.Admin.Controllers
{
  
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _he;

        
        public ProductsController(ApplicationDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }

        public IActionResult Index()
        {
            return View(_db.products.Include(c=>c.producttype).Include(c=>c.SpecialTag).ToList());
        }

        public IActionResult Create()
        {
            ViewData["prodtypeid"] = new SelectList(_db.producttype.ToList(),"ID","prodtype");
            ViewData["SpecialTagid"] = new SelectList(_db.SpecialTag.ToList(), "Id", "SpecialTagname");
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        
        public async Task<IActionResult> Create(products products,IFormFile image)
        {   
            if(ModelState.IsValid)
            {
                if(image!=null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "images/"+image.FileName;
                }
                _db.products.Add(products);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product has been saved";
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        public IActionResult Edit(int? id)
        {
            ViewData["producttypeid"] = new SelectList(_db.producttype.ToList(),"ID","prodtype");
            ViewData["Specialtagid"] = new SelectList(_db.SpecialTag.ToList(),"Id", "SpecialTagname");
            if(id==null)
            {
                return NotFound();
            }
            var product = _db.products.Include(c => c.producttype).Include(c => c.SpecialTag).FirstOrDefault(c=>c.Id==id);
            ViewData["image"] = product.Image;
            if (product==null) 
            {
                return NotFound();
            }


            return View(product);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(products products,IFormFile pic)
        {   if(ModelState.IsValid)
            {   if(pic!=null)
                {
                    var name = Path.Combine(_he.WebRootPath+"/images",Path.GetFileName(pic.FileName));
                    await pic.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image ="images/"+ pic.FileName;
                }

                _db.products.Update(products);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Product has been changed";
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }
        
        public IActionResult Details(int? id)
        {
            if(id==null)
            {
                return NotFound();
                    
            }
            var products = _db.products.Include(c => c.producttype).Include(m => m.SpecialTag).FirstOrDefault(n => n.Id == id);
            if(products==null)
            {
                return NotFound();
            }
            return View(products);
        }
        public IActionResult Delete(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var products = _db.products.Include(c => c.producttype).Include(m => m.SpecialTag).FirstOrDefault(n => n.Id == id);
            
            if (products==null)
            {
                return NotFound();
            }
            return View(products);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(products products) 
        { 
            if(ModelState.IsValid)
            {
                _db.products.Remove(products);
                await _db.SaveChangesAsync();
                TempData["delet"] = "Product has been deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }
    }
}
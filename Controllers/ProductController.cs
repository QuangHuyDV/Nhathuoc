using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nhathuoc.Data;
using Nhathuoc.Models;

namespace Nhathuoc.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {

        private readonly PharmacyContext db;

        public ProductController(PharmacyContext context)
        {
            db = context;
        }

        // GET: Product
        [HttpGet("List")]
        public async Task<IActionResult> Index()
        {
            var products = await db.Products.Include(p => p.Category).ToListAsync();
            return View(products);
        }

        // GET: Product/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Manufacturer,Price,ExpiryDate,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                db.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        [HttpGet("Update")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Manufacturer,Price,ExpiryDate,CategoryId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    product.Updated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    db.Update(product);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }


        private bool ProductExists(int id)
        {
            return db.Products.Any(e => e.ProductId == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
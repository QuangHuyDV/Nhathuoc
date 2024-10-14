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
    [Route("[controller]")]
    public class StockController : Controller
    {
        private readonly PharmacyContext db;

        public StockController(PharmacyContext context)
        {
            db = context;
        }

        private int pageSize = 10;

        // GET: Stock
        [HttpGet("List")]
        public IActionResult Index(int? mid, int page = 1)
        {
            var stocks = (IQueryable<Stock>)db.Stocks.Include(s => s.Supplier).Include(p => p.Product);
            if (mid != null)
            {
                stocks = stocks.Where(p => p.StockId == mid).Include(s => s.Supplier).Include(p => p.Product);
                ViewBag.mid = mid;
            }

            int totalItems = stocks.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (float)pageSize);
            ViewBag.pageNum = totalPages;
            ViewBag.currentPage = page;

            var result = stocks.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return View(result);
        }

        // GET: Stock/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(db.Suppliers, "SupplierId", "Name");
            ViewData["ProductId"] = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        // POST: Stock/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockId,ProductId,SupplierId,QuantityReceived,ReceivedDate")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                stock.Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                db.Add(stock);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(db.Suppliers, "SupplierId", "Name", stock.SupplierId);
            ViewData["ProductId"] = new SelectList(db.Products, "ProductId", "Name", stock.ProductId);
            return View(stock);
        }

        // GET: Stock/Edit/5
        [HttpGet("Update")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await db.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(db.Suppliers, "SupplierId", "Name", stock.SupplierId);
            ViewData["ProductId"] = new SelectList(db.Products, "ProductId", "Name");
            return View(stock);
        }

        // POST: Stock/Edit/5
        [HttpPost("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockId,ProductId,SupplierId,QuantityReceived,ReceivedDate")] Stock stock)
        {
            if (id != stock.StockId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    stock.Updated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    db.Update(stock);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.StockId))
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
            ViewData["SupplierId"] = new SelectList(db.Suppliers, "SupplierId", "Name", stock.SupplierId);
            ViewData["ProductId"] = new SelectList(db.Products, "ProductId", "Name", stock.ProductId);
            return View(stock);
        }

        private bool StockExists(int id)
        {
            return db.Stocks.Any(e => e.StockId == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
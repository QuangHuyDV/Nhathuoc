using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nhathuoc.Data;
using Nhathuoc.Models;

namespace Nhathuoc.Controllers
{
    [Route("[controller]")]
    public class SupplierController : Controller
    {

        private readonly PharmacyContext db;

        public SupplierController(PharmacyContext context)
        {
            db = context;
        }
        private int pageSize = 10;

        // GET: Supplier
        [HttpGet("List")]
        public IActionResult Index(int? mid, int page = 1)
        {
            var suppliers = (IQueryable<Supplier>)db.Suppliers;
            if (mid != null)
            {
                suppliers = suppliers.Where(p => p.SupplierId == mid);
                ViewBag.mid = mid;
            }

            int totalItems = suppliers.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (float)pageSize);
            ViewBag.pageNum = totalPages;
            ViewBag.currentPage = page;

            var result = suppliers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return View(result);
        }

        // GET: Supplier/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierId,SupplierName,Email,Phone,Address")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                db.Add(supplier);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Supplier/Edit/5
        [HttpGet("Update")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await db.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        [HttpPost("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,SupplierName,Email,Phone,Address")] Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    supplier.Updated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    db.Update(supplier);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(supplier.SupplierId))
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
            return View(supplier);
        }

        private bool CustomerExists(int id)
        {
            return db.Suppliers.Any(e => e.SupplierId == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
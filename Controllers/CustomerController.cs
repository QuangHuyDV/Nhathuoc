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
    public class CustomerController : Controller
    {
        private readonly PharmacyContext db;

        public CustomerController(PharmacyContext context)
        {
            db = context;
        }

        private int pageSize = 10;

        // GET: Customer
        [HttpGet("List")]
        public IActionResult Index(int? mid, int page = 1)
        {
            IQueryable<Customer> cutomers = db.Customers;
            if (mid != null)
            {
                cutomers = cutomers.Where(c => c.CustomerId == mid);
                ViewBag.mid = mid;
            }

            int totalItems = cutomers.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (float)pageSize);
            ViewBag.pageNum = totalPages;
            ViewBag.currentPage = page;

            var result = cutomers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return View(result);
        }

        // GET: Customer/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,CustomerEmail,CustomerPhone,CustomerAddress,Dob")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                db.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        [HttpGet("Update")]
        public async Task<IActionResult> Edit(int? mid)
        {
            if (mid == null)
            {
                return NotFound();
            }

            var customer = await db.Customers.FindAsync(mid);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int mid, [Bind("CustomerId,CustomerName,CustomerEmail,CustomerPhone,CustomerAddress,Dob")] Customer customer)
        {
            if (mid != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.Updated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    db.Update(customer);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await db.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await db.Customers.FindAsync(id);
            db.Customers.Remove(customer);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Any(e => e.CustomerId == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
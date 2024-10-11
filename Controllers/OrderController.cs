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
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        private readonly PharmacyContext db;

        public OrderController(PharmacyContext context)
        {
            db = context;
        }

        // GET: Order
        // GET: Order
        public async Task<IActionResult> Index()
        {
            var orders = await db.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetail)
                .ThenInclude(od => od.Product)
                .ToListAsync();
            return View(orders);
        }


        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(db.Customers, "CustomerId", "Name");
            ViewData["ProductId"] = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,OrderDate,TotalAmount,Status,Created,Updated,OrderDetail")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(db.Customers, "CustomerId", "Name", order.CustomerId);
            ViewData["ProductId"] = new SelectList(db.Products, "ProductId", "ProductName");
            return View(order);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await db.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetail)
                .ThenInclude(od => od.Product) // Include Product information
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Edit/5
        // POST: Order/UpdateStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string newStatus)
        {
            var order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = newStatus;
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool OrderExists(int id)
        {
            return db.Orders.Any(e => e.OrderId == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
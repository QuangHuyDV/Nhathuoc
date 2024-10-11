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
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        private PharmacyContext db;

        public CategoryController(PharmacyContext context)
        {
            db = context;
        }

        private int pageSize = 10;

        [HttpGet("List")]
        public IActionResult Index(int? mid, int currentPage)
        {
            IQueryable<Category> categorys = db.Categories;
            if (mid != null)
            {
                categorys = db.Categories.Where(c => c.CategoryId == mid);
            }
            int totalItems = categorys.Count();
            int pageNum = (int)Math.Ceiling(totalItems / (float)pageSize);
            ViewBag.pageNum = pageNum;
            var result = categorys.Skip((pageSize - 1) * pageSize).Take(pageSize).ToList();
            return View(result);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet("Update")]
        private IActionResult Edit(int id)
        {
            if (id == 0 || db.Categories == null)
            {
                return NotFound();
            }
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        private IActionResult Edit(int id, [Bind("Name,Description")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    category.Updated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    db.Update(category);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!db.Categories.Any(e => e.CategoryId == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet("Find")]
        public IActionResult FindCategory(int? mid, string? key, int? pageIndex)
        {
            var categorys = (IQueryable<Category>)db.Categories;
            int page = (int)(pageIndex == null || pageIndex <= 0 ? 1 : pageIndex);
            if (mid == null)
            {
                categorys = categorys.Where(e => e.CategoryId == mid);
                ViewBag.mid = mid;
            }
            if (key != null)
            {
                categorys = categorys.Where(c => c.Name.ToLower().Contains(key.ToLower()));
                ViewBag.key = key;
            }
            int pageNum = (int)Math.Ceiling(categorys.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;
            var result = categorys.Skip(pageSize * (page - 1)).Take(pageSize);
            return PartialView("CategoryTable", result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
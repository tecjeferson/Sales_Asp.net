using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace aspnetapp.Controllers
{

    public class SalesPersonController : Controller
    {
        private readonly SalesContext _context;
        public SalesPersonController(SalesContext context)
        {
            _context = context;

            if (_context.SalespersonData.Count() == 2)
            {
                _context.SalespersonData.AddRange(new Sales
                {
                    Salesperson = "Jeferson",
                }, new Sales
                {
                    Salesperson = "Ryan",
                });
                _context.SaveChanges();
            }
        }
        public IActionResult Index()
        {
            List<SalesPerson> salesPerson = Task.Run(GetSalesperson).Result.Value.ToList();

            return View(salesPerson);
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesPerson>>> GetSalesperson()
        {
            return await _context.SalespersonData.ToListAsync();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Sales sales)
        {
            await _context.SalespersonData.AddAsync(sales);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(long? id)
        {
            var item = _context.SalespersonData.SingleOrDefault(s => s.Id == id);
            return View(item);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long? id, SalesPerson slPerson)
        {
            var salesId = _context.SalespersonData.Find(id);

            salesId.Salesperson = slPerson.Salesperson;

            _context.SalespersonData.Update(salesId);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long? id)
        {
            var item = _context.SalespersonData.Find(id);
            return View(_context.SalespersonData.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Sales sales)
        {


            _context.SalespersonData.Remove(sales);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
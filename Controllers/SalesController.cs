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
    
    public class SalesController : Controller
    {
        private readonly SalesContext _context;

        public SalesController(SalesContext context)
        {
            _context = context;

            if (_context.SalesData.Count() == 0)
            {
                _context.SalesData.AddRange(new Sales
                {
                    CustomerName = "Aveva Inc.",
                    Salesperson = "Charlie",
                    hasPayment = true,
                    Price = 199
                }, new Sales {
                    CustomerName = "Netflix",
                    Salesperson = "Doug",
                    hasPayment = false,
                    Price = 13
                });
                _context.SaveChanges();
            }
        }
        public IActionResult Index()
        {
            List<Sales> sales = Task.Run(GetSales).Result.Value.ToList();
            
            return View(sales);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Salesperson()
        {
            return View();
        }


        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sales>>> GetSales()
        {
            return await _context.SalesData.ToListAsync();
        }

        // GET: api/Sales/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Sales>> GetSalesId(long id)
        {
            var salesItem = await _context.SalesData.FindAsync(id);

            if (salesItem == null)
            {
                return NotFound();
            }

            return salesItem;
        }
    }
}
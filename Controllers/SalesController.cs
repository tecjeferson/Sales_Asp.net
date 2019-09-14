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
                }, new Sales
                {
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
        public IActionResult Create()
        {
            var sales = _context.SalesData.OrderBy(i => i.Salesperson).ToList();
            sales.Insert(0, new Sales() { Id = 0, Salesperson = "" });
            ViewBag.Sales = sales;

            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sales sales)
        {

            await _context.SalesData.AddAsync(sales);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        //Redireciona para a pagina EDIT com os dados
        public async Task<ActionResult> Edit(long? id)
        {
            var item = await _context.SalesData.SingleOrDefaultAsync(s => s.Id == id);

            return View(item);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(long? id, Sales sl)
        {

            var vendas = _context.SalesData.Find(id);

            vendas.CustomerName = sl.CustomerName;
            vendas.Salesperson = sl.Salesperson;
            vendas.hasPayment = sl.hasPayment;
            vendas.Price = sl.Price;

            _context.SalesData.Update(vendas);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(long? id)
        {
            var item = _context.SalesData.Find(id);
            return View(_context.SalesData.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Sales sales)
        {


            _context.SalesData.Remove(sales);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
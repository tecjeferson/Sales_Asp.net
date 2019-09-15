using Microsoft.AspNetCore.Mvc;
using aspnet.Services.Implementations;
using aspnetapp.Models;
using aspnet.Services;
using System.Linq;

namespace aspnet.Controllers
{
    public class SalesApiController
    {

        [Route("api/[controller]")]
        public class SalesController : Controller
        {
            private ISalesService _salesService;
            public SalesController(ISalesService salesService)
            {
                _salesService = salesService;
            }

            public IActionResult Index()
            {
                var sales = _salesService.FindAll();

                return View(sales);
            }
            [HttpGet]
            public IActionResult Get()
            {
                return Ok(_salesService.FindAll());
            }

            [HttpGet("{id}")]
            public IActionResult Get(long? id)
            {
                var sale = _salesService.FindById(id);
                if (sale == null) return NotFound();
                return Ok(sale);
            }


            public IActionResult Create()
            {
                var sales = _salesService.FindAll().OrderBy(i => i.Salesperson).ToList();
                sales.Insert(0, new Sales() { Id = 0, Salesperson = "" });
                ViewBag.Sales = sales;

                return View();
            }

            [HttpPost, ActionName("Create")]
            [ValidateAntiForgeryToken]
            public IActionResult Create(Sales sales)
            {

                _salesService.Create(sales);

                return RedirectToAction("Index");

            }


            //Redireciona para a pagina EDIT com os dados
            public ActionResult Edit(long? id)
            {
                var item = _salesService.FindAll().SingleOrDefault(s => s.Id == id);

                return View(item);
            }

            [HttpPost, ActionName("Edit")]
            [ValidateAntiForgeryToken]
            public ActionResult EditPost(long? id, Sales sl)
            {

                var vendas = _salesService.FindById(id);

                vendas.CustomerName = sl.CustomerName;
                vendas.Salesperson = sl.Salesperson;
                vendas.hasPayment = sl.hasPayment;
                vendas.Price = sl.Price;

                _salesService.Update(vendas);

                return RedirectToAction("Index");

            }

            public ActionResult Delete(long? id)
            {
                var item = _salesService.FindById(id);
                return View(_salesService.FindById(id));
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Delete(long id)
            {
                _salesService.Delete(id);

                return RedirectToAction("Index");
            }


            // GET: api/Sales
            // [HttpGet]
            // public ActionResult GetSales()
            // {
            //     return _salesService.FindAll();
            // }
            // // GET: api/Sales/id
            // [HttpGet("{id}")]
            // public async Task<ActionResult<Sales>> GetSalesId(long id)
            // {
            //     var salesItem = await _salesService.FindAsync(id);

            //     if (salesItem == null)
            //     {
            //         return NotFound();
            //     }

            //     return salesItem;
            // }
            // public IActionResult Create()
            // {
            //     var sales = _salesService.FindAll().OrderBy(i => i.Salesperson).ToList();
            //     sales.Insert(0, new Sales() { Id = 0, Salesperson = "" });
            //     ViewBag.Sales = sales;

            //     return View();
            // }

            // [HttpPost, ActionName("Create")]
            // [ValidateAntiForgeryToken]
            // public async Task<IActionResult> Create(Sales sales)
            // {

            //     await _salesService.AddAsync(sales);
            //     await _context.SaveChangesAsync();

            //     return RedirectToAction("Index");

            // }

            //Redireciona para a pagina EDIT com os dados
            // public async Task<ActionResult> Edit(long? id)
            // {
            //     var item = await _salesService.SingleOrDefaultAsync(s => s.Id == id);

            //     return View(item);
            // }

            // [HttpPost, ActionName("Edit")]
            // [ValidateAntiForgeryToken]
            // public ActionResult EditPost(long? id, Sales sl)
            // {

            //     var vendas = _salesService.FindZce = sl.Price;

            //     _salesService.Update(vendas);
            //     _context.SaveChanges();
            //     return RedirectToAction("Index");

            // }

            // public ActionResult Delete(long? id)
            // {
            //     var item = _salesService.Find(id);
            //     return View(_salesService.Find(id));
            // }

            // [HttpPost]
            // [ValidateAntiForgeryToken]
            // public ActionResult Delete(Sales sales)
            // {


            //     _salesService.Remove(sales);
            //     _context.SaveChanges();
            //     return RedirectToAction("Index");
            // }


        }
    }
}
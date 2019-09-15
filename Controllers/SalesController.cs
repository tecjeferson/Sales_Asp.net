using Microsoft.AspNetCore.Mvc;
using aspnet.Services.Implementations;
using aspnetapp.Models;
using aspnet.Services;
using System.Linq;
using System.Collections.Generic;

namespace aspnetapp.Controllers
{
    public class SalesController : Controller
    {
        //injection dependence from ISalesService
        private ISalesService _salesService;
        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        //IAction for Index View in Sales folder
        public IActionResult Index()
        {
            var sales = _salesService.FindAll();

            return View(sales);
        }

        //Returns a Json from In Memory Database
        [HttpGet("api/[controller]")]
        public IActionResult Get()
        {
            return Ok(_salesService.FindAll());
        }
        //Returns a Json by ID parameter from In Memory Database
        [HttpGet("api/[controller]/{id}")]
        public IActionResult Get(long? id)
        {
            var sale = _salesService.FindById(id);
            if (sale == null) return NotFound();
            return Ok(sale);
        }



        //Update sending data by Json
        [HttpPut("api/[controller]/{id}")]
        public IActionResult Put([FromBody]Sales sales)
        {
            if (sales == null) return BadRequest();
            return new ObjectResult(_salesService.Update(sales));
        }
        //Redireciona para a pagina EDIT com os dados
        public ActionResult Edit(long? id)
        {
            var sales = _salesService.FindAll().OrderBy(i => i.Salesperson).ToList();
            sales.Insert(0, new Sales() { Id = 0, Salesperson = "" });
            ViewBag.Sales = sales;
            var item = _salesService.FindAll().SingleOrDefault(s => s.Id == id);

            return View(item);
        }
        //
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





        //Creates sending data by Json
        [HttpPost("api/[controller]")]
        public IActionResult Post([FromBody]Sales sales)
        {
            if (sales == null) return BadRequest();
            return new ObjectResult(_salesService.Create(sales));
        }

        //Return to Create.cshtml Salesperson data and redirect to View page
        public IActionResult Create()
        {
            var sales = _salesService.FindAll().OrderBy(i => i.Salesperson).ToList();
            sales.Insert(0, new Sales() { Id = 0, Salesperson = "" });
            ViewBag.Sales = sales;

            return View();
        }

        //Creates by Create.cshtml in View folder
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Sales sales)
        {
            _salesService.Create(sales);
            return RedirectToAction("Index");
        }


        [HttpDelete("api/[controller]/{id}")]
        public IActionResult Del(long id)
        {
            _salesService.Delete(id);
            return NoContent();
        }


        public ActionResult Delete(long? id)
        {
            //var item = _salesService.FindById(id);
            return View(_salesService.FindById(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            _salesService.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Dashboard()
        {
            // var sales = _salesService.FindAll().OrderBy(i => i.Salesperson).ToList();
            // sales.Insert(0, new Sales() { Id = 0, Salesperson = "" });
            // ViewBag.Sales = sales;
            // var sales = _salesService.FindAll();
            // List<int> repartitions = new List<int>();
            // var slPerson = _salesService.Find(x => x.Salesperson).Distinct();




            return View();
        }



    }
}
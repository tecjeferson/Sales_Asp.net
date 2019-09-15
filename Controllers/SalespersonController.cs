using Microsoft.AspNetCore.Mvc;
using aspnet.Services.Implementations;
using aspnetapp.Models;
using aspnet.Services;
using System.Linq;
using System.Collections.Generic;

namespace aspnetapp.Controllers
{

    public class SalesPersonController : Controller
    {
        public ISalesPersonService _contextSP;
        public SalesPersonController(ISalesPersonService salesService)
        {
            _contextSP = salesService;

        }
        //IAction for Index View in Sales folder
        public IActionResult Index()
        {

            List<SalesPerson> salesPerson = _contextSP.FindAll();

            return View(salesPerson);
        }

        //Returns a Json from In Memory Database
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_contextSP.FindAll());
        }

        //Returns a Json by ID parameter from In Memory Database
        [HttpGet("api/[controller]/{id}")]
        public IActionResult Get(long? id)
        {
            var salesPerson = _contextSP.FindById(id);
            if (salesPerson == null) return NotFound();
            return Ok(salesPerson);
        }



        public ActionResult Edit(long? id)
        {
            var item = _contextSP.FindById(id);
            return View(item);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long? id, SalesPerson slPerson)
        {
            var salesId = _contextSP.FindById(id);

            salesId.Salesperson = slPerson.Salesperson;

            _contextSP.Update(salesId);
            return RedirectToAction("Index");
        }





        // //Creates sending data by Json
        // [HttpPost("api/[controller]")]
        // public IActionResult Post([FromBody]SalesPerson salesperson)
        // {
        //     if (salesperson == null) return BadRequest();
        //     return new ObjectResult(_salesPersonService.Create(salesperson));
        // }

        // //Return to Create.cshtml Salesperson data and redirect to View page
        // public IActionResult Create()
        // {
        //     var sales = _salesPersonService.FindAll().OrderBy(i => i.Salesperson).ToList();
        //     sales.Insert(0, new SalesPerson() { Id = 0, Salesperson = "" });
        //     ViewBag.Sales = sales;

        //     return View();
        // }

        // //Creates by Create.cshtml in View folder
        // [HttpPost, ActionName("Create")]
        // [ValidateAntiForgeryToken]
        // public IActionResult Create(SalesPerson salesperson)
        // {
        //     _salesPersonService.Create(salesperson);
        //     return RedirectToAction("Index");
        // }


        // [HttpDelete("api/[controller]/{id}")]
        // public IActionResult Del(long id)
        // {
        //     _salesPersonService.Delete(id);
        //     return NoContent();
        // }


        // public ActionResult Delete(long? id)
        // {
        //     //var item = _salesPersonService.FindById(id);
        //     return View(_salesPersonService.FindById(id));
        // }


        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Delete(long id)
        // {
        //     _salesPersonService.Delete(id);

        //     return RedirectToAction("Index");
        // }



    }
}
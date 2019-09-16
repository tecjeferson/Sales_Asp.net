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
        private ISalesService _contextSP;
        public SalesPersonController(ISalesService salesService)
        {
            _contextSP = salesService;

        }
        //IAction for Index View in Sales folder
        public IActionResult Index()
        {

            var salesPerson = _contextSP.FindAll();

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





        //Creates sending data by Json
        [HttpPost("api/[controller]")]
        public IActionResult Post([FromBody]Sales sales)
        {
            if (sales == null) return BadRequest();
            return new ObjectResult(_contextSP.Create(sales));
        }

        //Return to Create.cshtml Salesperson data and redirect to View page
        public IActionResult Create()
        {

            return View();
        }

        // //Creates by Create.cshtml in View folder
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Sales sales)
        {
            _contextSP.Create(sales);
            return RedirectToAction("Index");
        }


        [HttpDelete("api/[controller]/{id}")]
        public IActionResult Del(long id)
        {
            _contextSP.Delete(id);
            return NoContent();
        }


        public ActionResult Delete(long? id)
        {
            //var item = _salesPersonService.FindById(id);
            return View(_contextSP.FindById(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            _contextSP.Delete(id);

            return RedirectToAction("Index");
        }



    }
}
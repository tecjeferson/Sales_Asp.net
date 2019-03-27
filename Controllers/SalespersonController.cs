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
    
    public class SalespersonController : Controller
    {
        public SalespersonController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
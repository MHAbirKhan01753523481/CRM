using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMaking.Data;
using ProjectMaking.UnitOfWorks;

namespace ProjectMaking.Controllers
{
    public class DashBordController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly AppDbContext _context;
        public DashBordController(IUnitOfWork work, AppDbContext context)
        {
            _work = work;
            _context = context;
        }
        public IActionResult GetClient()
        {
            ViewBag.item = _work.Clients.GetAll().Where(m => m.Country == "Bangladesh").Count();
            return View();
        }
       
        public IActionResult Index()
        {
            ViewBag.items = _work.Clients.GetAll().Count();
            ViewBag.item = _work.Clients.GetAll().Where(m => m.Country == "Bangladesh").Count();
            ViewBag.country = _work.Clients.GetAll().Where(x => x.Country == "USA").Count();
            ViewBag.country1 = _work.Clients.GetAll().Where(x => x.Country == "India").Count();
            ViewBag.leads = _work.Clients.GetAll().Where(x => x.StatusType == "105" && x.StatusType== "103").Count();
            ViewBag.itemLead = _work.Clients.GetAll().Where(m => m.StatusType == "105").Count();

            ViewBag.itemList = _work.Clients.GetAll();

            ViewBag.sales = _work.Sales.GetAll();
            ViewBag.workTask = _work.WorkTask.GetAllWithWorkTaskData();
               
            return View();
        }

        public IActionResult Details()
        {
            ViewBag.workTask = _work.WorkTask.GetAllWithWorkTaskData();
            return View();
        }
    }
}
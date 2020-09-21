using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMaking.Data;
using ProjectMaking.Helper;
using ProjectMaking.Models.EntityModel;
using ProjectMaking.Models.ViewModel;
using ProjectMaking.UnitOfWorks;

namespace ProjectMaking.Controllers
{
    public class SalesmanController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly IImagePath _imagePath;
        private AppDbContext _context;

        public SalesmanController(IUnitOfWork work, IImagePath imagePath,AppDbContext context)
        {
            _work = work;
            _imagePath = imagePath;
            _context = context;
        }
        // GET: Salesman
        public IActionResult Index()
        {
            ViewBag.client =new SelectList(_work.Clients.GetAll(),"Id","Name");
            ViewBag.manager =new SelectList(_work.Managers.GetAll(),"Id","Name");
            ViewBag.shedule =new SelectList(_work.Sehedules.GetAll(),"Id", "Type");
            ViewBag.product =new SelectList(_work.Products.GetAll(),"Id","Name");
          
            return View();
        }

        // GET: Salesman/Details/5

        // GET: Salesman/Create
        public IActionResult Create(vmSalesMan vmSales)
        {
            if (ModelState.IsValid)
            {
                SalesMan sales = new SalesMan()
                {
                    Name = vmSales.Name,
                    Email = vmSales.Email,
                    Address = vmSales.Address,
                    Phone = vmSales.Phone,
                    ClientId = vmSales.ClientId,
                  //  ManagerId = vmSales.ManagerId,
                    ProductId = vmSales.ProductId,
                    SeheduleId = vmSales.SeheduleId,
                    IconClass = vmSales.IconClass,
                    IconColor = vmSales.IconColor,
                    DateTime = vmSales.DateTime != null ? vmSales.DateTime : DateTime.Now,
                    Pass = vmSales.Pass
               
                };

                if (vmSales.Image != null)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(vmSales.Image.ContentDisposition).FileName.Trim('"').Replace(" ", string.Empty);
                    var path = _imagePath.GetImagePath(fileName, "Sales", vmSales.Name.Replace(" ", string.Empty));

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        vmSales.Image.CopyTo(stream);
                    }
                    sales.Image = _imagePath.GetImagePathForDb(path);
                }
                _work.Sales.Add(sales);
                _work.Save();

                return Json(true);
            }

            return Json(false);
        }

        [HttpGet]
        public IActionResult TaskModified(int id)
        {
            var salesVm = _context.SalesMen.Find(id);
            vmSalesMan salesMan = new vmSalesMan();
            salesMan.Description = salesVm.Description;
            salesMan.TaskName = salesVm.TaskName;
           // salesMan.ManagerId = salesVm.ManagerId;
            ViewBag.manager = new SelectList(_work.Managers.GetAll(), "Id", "Name");
            return PartialView("_SalesTaskModified", salesMan);
        }
        public IActionResult LoadSales()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            List<SalesMan> salesMenList = _work.Sales.GetAllSalesJoinData();
            List<vmSalesMan> salesItem = new List<vmSalesMan>();
            foreach (var item in salesMenList)
            {
                salesItem.Add(new vmSalesMan()
                {
                    Id=item.Id,
                    Name=item.Name,
                    Email=item.Email,
                    Address=item.Address,
                    Phone=item.Phone,
                    Pass=item.Pass,
                    ClientName=item.Client.Name,
                  //  ManagerName=item.Managers.Name,
                    ProductName=item.Products.Name,
                    SeheduleName=item.Sehedule.Type,
                    Comment=item.Comment,
                    Replay=item.Replay,
                    ChangeDateTime=item.DateTime.ToString("dddd dd MMMMM yyyy"),
                    Mark=item.Mark,
                    Description=item.Description,
                    Photo=item.Image,
                    IconClass=item.IconClass,
                    IconColor=item.IconColor,
                    TaskName=item.TaskName,
                   
                });
            }
            

           

            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
            {
                // ClientList = ClientList.AsQueryable().OrderBy(sortColumn + "" + sortColumnDir).ToList();
            }
            else
            {
                salesItem = salesItem.OrderByDescending(m => m.Id).ToList();
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                salesItem = salesItem.Where(m => m.Name.Contains(searchValue) || m.Email.Contains(searchValue) || m.Phone.Contains(searchValue) || m.TaskName.Contains(searchValue)).ToList();
            }

            //total number of rows count     
            recordsTotal = salesItem.Count();

            //Paging     
            var data = salesItem.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        // POST: Salesman/Create
      
        // GET: Salesman/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Salesman/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Salesman/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Salesman/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
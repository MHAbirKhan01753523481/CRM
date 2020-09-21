using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectMaking.Models.EntityModel;
using ProjectMaking.Models.ViewModel;
using ProjectMaking.UnitOfWorks;

namespace ProjectMaking.Controllers
{
    public class SalesReportController : Controller
    {
        private readonly IUnitOfWork _work;

        public SalesReportController(IUnitOfWork work)
        {
            _work = work;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadWorkTask()
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

            List<WorkTask> workTaskList = _work.WorkTask.GetAllWithWorkTaskData();
            List<vmWorkTask> workTaskItem = new List<vmWorkTask>();
            foreach (var item in workTaskList)
            {
                workTaskItem.Add(new vmWorkTask()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ToDate = item.ToDate,
                    EndDate = item.EndDate,
                    Mark = item.Mark,
                    Comment = item.Comment,
                    Reply = item.Reply,
                    Requirement = item.Requirement,
                    CompliteTime = item.CompliteTime,
                    ManagerName = item.Manager.Name,
                    SalesManName = item.SalesMan.Name,
                    ManagerId = item.ManagerId,
                    SalesManId = item.SalesManId,
                    PaidAmmount = item.PaidAmmount,
                    DewAmmount = item.DewAmmount,
                });
            }

            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
            {
                // ClientList = ClientList.AsQueryable().OrderBy(sortColumn + "" + sortColumnDir).ToList();
            }
            else
            {
                workTaskItem = workTaskItem.OrderByDescending(m => m.Id).ToList();
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                workTaskItem = workTaskItem.Where(m => m.Name.Contains(searchValue)).ToList();
            }

            //total number of rows count     
            recordsTotal = workTaskItem.Count();

            //Paging     
            var data = workTaskItem.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
    }
}
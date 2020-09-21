using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMaking.Data;
using ProjectMaking.Helper;
using ProjectMaking.Models.EntityModel;
using ProjectMaking.Models.ViewModel;
using ProjectMaking.UnitOfWorks;

namespace ProjectMaking.Controllers
{
    public class WorkTaskController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly AppDbContext  _context;
      
        public WorkTaskController(IUnitOfWork work, AppDbContext context)
        {
            _work = work;
            _context = context;
        }
        // GET: WorkTask


        public ActionResult Index()
        {
            ViewBag.manager = new SelectList(_work.Managers.GetAll(), "Id", "Name");
            ViewBag.salesMan = new SelectList(_work.Sales.GetAll(), "Id", "Name");
            ViewBag.IconClass = _work.dropdown.GetIconClass();
            ViewBag.IconColor = _work.dropdown.GetIconColor();
            WorkTask workTask = new WorkTask();
           
           
            return View();
        }

        public async Task<IActionResult> TaskList(int page=1)
        {

            int pageSize = 6;
           // ViewBag.result = _work.dataTableList.GetWorkData();
            ViewBag.count = _work.WorkTask.GetAll().Count();

            //ViewBag.completetask = _work.WorkTask.GetAll().Where(x=>x.Comment !=null && x.Mark.ToString() !=null).Count();
            ViewBag.completetask = _work.WorkTask.GetCompleteTask();

            //ViewBag.activeTask = _work.WorkTask.GetAll().Where(x => x.Reply != null).Count();

            ViewBag.activeTask = _work.WorkTask.GetActiveTask();

            // ViewBag.comment = _work.WorkTask.GetAll().Where(x => x.Comment !=null).Count();

            ViewBag.comment = _work.WorkTask.GstAllComments();
           
            return View( await PaginatedList<WorkTask>.CreateAsync(_context.ManagerTasks.AsNoTracking(), page, pageSize));
        }

      

        public IActionResult SalesTaskIndex()
        {
            ViewBag.msg = _work.WorkTask.GetAll();
            return View();
        }
        public IActionResult SalesTask(int id)
        {
            var workTotal = _work.WorkTask.Get(id);
            vmWorkTask vmWorkTask = new vmWorkTask();
            vmWorkTask.Reply = workTotal.Reply;
            vmWorkTask.CompliteTime = workTotal.CompliteTime;
            vmWorkTask.PaidAmmount = workTotal.PaidAmmount;
            vmWorkTask.DewAmmount = workTotal.DewAmmount;
          
            return PartialView("_WorkTask", vmWorkTask);
        }
        [HttpPost]
        public IActionResult WorkTaskReplay(vmWorkTask vmWorkTask)
        {
            var taskItem = _work.WorkTask.Get(vmWorkTask.Id);
            if (taskItem !=null)
            {
               
                taskItem.Reply = vmWorkTask.Reply;
                taskItem.CompliteTime = vmWorkTask.ChangeCompliteTime != null ? vmWorkTask.CompliteTime:DateTime.Now;
                taskItem.DewAmmount = vmWorkTask.DewAmmount;
                taskItem.PaidAmmount = vmWorkTask.PaidAmmount;
                
                _work.WorkTask.Update(taskItem);
                _work.Save();
                return Json(true);
            }
           
            return Json(false);
        }
        [HttpPost]
        public IActionResult SaveData(vmWorkTask vmWorkTask)
        {
           
            if (ModelState.IsValid)
            {
                    WorkTask task = new WorkTask()
                    {
                        Name = vmWorkTask.Name,
                        Requirement = vmWorkTask.Requirement,
                        ToDate = vmWorkTask.ToDate != null ? vmWorkTask.ToDate : DateTime.Now,
                        EndDate = vmWorkTask.EndDate != null ? vmWorkTask.EndDate : DateTime.Now,
                      //  FixedTime = vmWorkTask.FixedTime != null ? vmWorkTask.FixedTime : DateTime.Now,
                        ManagerId = vmWorkTask.ManagerId,
                        SalesManId = vmWorkTask.SalesManId,
                        IconClass = vmWorkTask.IconClass,
                        IconColor = vmWorkTask.IconColor,
                        };
                        _work.WorkTask.Add(task);
                        _work.Save();
                       
                return Json(true);
            }
           
            return Json(false);
        }
       
        public IActionResult UpdateWorkTask(vmWorkTask vmWorkTask)
        {
            var vmTaskObj = _work.WorkTask.Get(vmWorkTask.Id);
            if (vmTaskObj != null)
            {
                vmTaskObj.Name = vmWorkTask.Name;
                vmTaskObj.ToDate = vmWorkTask.ChangeToDate != null ? vmWorkTask.ToDate : DateTime.Now;
                vmTaskObj.EndDate = vmWorkTask.ChangeEndDate != null ? vmWorkTask.EndDate :DateTime.Now;
                vmTaskObj.IconClass = vmWorkTask.IconClass;
                vmTaskObj.IconColor = vmWorkTask.IconColor;
                vmTaskObj.ManagerId = vmWorkTask.ManagerId;
                vmTaskObj.SalesManId = vmWorkTask.SalesManId;
                vmTaskObj.Requirement = vmWorkTask.Requirement;

                _work.WorkTask.Update(vmTaskObj);
                _work.Save();
                 return Json(true);
            }
            return Json(false);
        }
        public IActionResult ReplayeView(int id)
        {
            var replayeTask = _work.WorkTask.Get(id);
            vmWorkTask replayeObj = new vmWorkTask();
            replayeObj.Mark = replayeTask.Mark;
            replayeObj.Comment = replayeTask.Comment;
            return PartialView("_Replay", replayeObj);
        }
        public IActionResult ReplayeUpdate(vmWorkTask workTask)
        {
            var replaye = _work.WorkTask.Get(workTask.Id);
            if (replaye !=null)
            {
                replaye.Mark =workTask.Mark==replaye.Mark ? replaye.Mark :workTask.Mark;
                replaye.Comment = workTask.Comment !=null ? workTask.Comment :replaye.Comment;
                _work.WorkTask.Update(replaye);
                _work.Save();
                if (workTask.Id>0)
                {
                    return Json(true);
                }
              
            }
            return Json(false);
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
                    Id=item.Id,
                    Name=item.Name,
                    ToDate=item.ToDate,
                    EndDate=item.EndDate,
                    ChangeEndDate=item.EndDate.ToString("dddd dd MMMMM yyyy"),
                    ChangeToDate=item.ToDate.ToString("dddd dd MMMMM yyyy "),
                    Mark=item.Mark,
                    Comment=item.Comment,
                    Reply=item.Reply,
                    Requirement=item.Requirement,
                    CompliteTime=item.CompliteTime,
                    ManagerName=item.Manager.Name,
                    SalesManName=item.SalesMan.Name,
                    ManagerId=item.ManagerId,
                    SalesManId=item.SalesManId,
                    PaidAmmount=item.PaidAmmount,
                    DewAmmount=item.DewAmmount,
                    IconClass=item.IconClass,
                    IconColor=item.IconColor,
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
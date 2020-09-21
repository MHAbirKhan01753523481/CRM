using Microsoft.EntityFrameworkCore;
using ProjectMaking.Data;
using ProjectMaking.Models.EntityModel;
using ProjectMaking.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectMaking.Helper
{
    public class DataTable
    {
        private readonly AppDbContext _context;
        //private readonly IImagePath _imagePath;

        public DataTable(AppDbContext context)
        {
            _context = context;
           
        }

        //public DataTable(AppDbContext context)
        //{
        //    _context = context;
        //}
         public  List<vmClient> GetVmClients()
        {
            List<Client> listOfClient = _context.Clients.ToList();
            List<vmClient> clientItem = new List<vmClient>();
            foreach (var item in listOfClient)
            {
                clientItem.Add(new vmClient()
                {
                    Name = item.Name,
                    Email = item.Email,
                    Address = item.Address,
                    CompanyName = item.CompanyName,
                    Country = item.Country,
                    Phone = item.Phone,
                    Id = item.Id,
                });
            }

            return clientItem;

        }

        public List<vmWorkTask> GetWorkData()
        {
            List<WorkTask> taskList = _context.ManagerTasks.Include(m => m.Manager).Include(s => s.SalesMan).ToList();
            List<vmWorkTask> taskItem = new List<vmWorkTask>();

            foreach (var item in taskList)
            {
                taskItem.Add(new vmWorkTask()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ToDate = item.ToDate,
                    EndDate = item.EndDate,
                    ChangeEndDate = item.EndDate.ToString("dddd dd MMMMM yyyy"),
                    ChangeToDate = item.ToDate.ToString("dddd dd MMMMM yyyy "),
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
                    IconClass = item.IconClass,
                    IconColor = item.IconColor,
                });
            }
            return taskItem;

            
        }

        public List<WorkTask> GetList()
        {
            // List<WorkTask> list = new List<WorkTask>();

            List<WorkTask> list =_context.ManagerTasks.ToList().ToList();

            return list;
        }

     
       
    }
}

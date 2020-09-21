using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectMaking.Data;
using ProjectMaking.Helper;
using ProjectMaking.Models.EntityModel;
using ProjectMaking.Models.ViewModel;
using ProjectMaking.UnitOfWorks;

namespace ProjectMaking.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly AppDbContext _context;
        private readonly IImagePath _imagePath;

        public ClientsController(IUnitOfWork work,IImagePath imagePath, AppDbContext context)
        {
            _work = work;
            _imagePath = imagePath;
            _context = context;

        }
        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult ClientList()
        {

            ViewBag.TotalLead = _work.Clients.TotalLead();
            ViewBag.LostLead = _work.Clients.LostLead();
            ViewBag.ActiveLead = _work.Clients.ActiveLead();

            //List<Client> client = _work.Clients.LeadManager();
            //ViewBag.leadmanagerItem = client;

            List<Manager> clients =_work.Managers.GetAll().Where(m=>m.Id==2009).ToList();
            //List<Manager> clients =_context.Manager.ToList().FirstOrDefault(x=>x.Id==x.Id);
            List<vmManager> clientitem = new List<vmManager>();
                foreach (var item in clients)
                {
                    string photoURL = "";
                    if (!string.IsNullOrEmpty(item.Image))
                    {
                        photoURL = _imagePath.GetFilePathAsSourceUrl(item.Image);
                    }
                    else
                    {
                        photoURL = _imagePath.GetFilePathAsSourceUrl("/images/ManagerUpload/");
                    }
                    clientitem.Add(new vmManager
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PhotoUrl = item.Image,
                        Designation=item.Designation,
                    });
                }

             ViewBag.leadmanagerItem = clientitem;

             ViewBag.image = _work.Clients.GetAll().Where(p => p.Image == p.Image || p.Id == 2007);

            return View();
        }

        //[HttpGet]
        //public IActionResult ManagerEditView(int id)
        //{
        //    var managerobj = _work.Clients.Get(id);
        //    vmClient client = new vmClient();
        //    client.Name = managerobj.Name;
        //    client.Photo = managerobj.Image;
        //  //  client.Id = managerobj.Id;
        //    return PartialView("_ManagerEditView", client);
        //}
        //[HttpPost]
        //public IActionResult ManagerEdit(vmClient vmClient)
        //{
        //    var clientObj = _work.Clients.Get(vmClient.Id);
        //    if (clientObj !=null)
        //    {
        //        clientObj.Name = vmClient.Name;

        //        if (vmClient.Image !=null)
        //        {
        //            var fileName = string.IsNullOrEmpty(Path.GetExtension(vmClient.Image.FileName)) ? ".jpg" : Path.GetExtension(vmClient.Image.FileName);
        //           var extn = vmClient.Id + fileName;
        //            var path = _imagePath.GetImagePath(extn, "Clients","Zahid");
        //            using (var stream=new FileStream (path, FileMode.Create))
        //            {
        //                vmClient.Image.CopyTo(stream);
        //            }
        //            clientObj.Image =_imagePath.GetImagePathForDb(path);
        //        }
        //        _work.Clients.Update(clientObj);
        //        _work.Save();
        //        return PartialView("_ManagerEditView", clientObj);
        //    }
        //    return PartialView("_ManagerEditView", vmClient);
        //}

        //public IActionResult ManagerDetails()
        //{
        //    List<Client> clients = _work.Clients.GetAll().Where(x => x.Id == 1002).ToList();
        //    List<vmClient> clientitem = new List<vmClient>();
        //    foreach (var item in clients)
        //    {
        //        string photoURL = "";
        //        if (!string.IsNullOrEmpty(item.Image))
        //        {
        //            photoURL = _imagePath.GetFilePathAsSourceUrl(item.Image);
        //        }
        //        else
        //        {
        //            photoURL = _imagePath.GetFilePathAsSourceUrl("/images/Clients/");
        //        }
        //        clientitem.Add(new vmClient
        //        {
        //            Id = item.Id,
        //            Name = item.Name,
        //            Photo = item.Image,
        //            Phone = item.Phone,
        //            Email = item.Email,
        //            Address = item.Address,
        //            CompanyName = item.CompanyName,
        //            Country = item.Country,
                    
        //        }) ;
        //    }

        //    ViewBag.leadmanagerItem = clientitem;

        //    //var managerDetails = _work.Clients.Get(id);
        //    //if (managerDetails ==null)
        //    //{
        //    //    return NotFound();
        //    //}
           
        //    return PartialView("_ManagerDetails");
        //}

        public IActionResult Create(vmClient vmClient)
        {
            if (ModelState.IsValid)
            {
                Client client = new Client()
                {
                    Name = vmClient.Name,
                    Address = vmClient.Address,
                    CompanyName = vmClient.CompanyName,
                    Country = vmClient.Country,
                    Email = vmClient.Email,
                    Phone = vmClient.Phone,
                    StatusType = vmClient.StatusType
                };
                if (vmClient.Image !=null)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(vmClient.Image.ContentDisposition).FileName.Trim('"').Replace(" ", string.Empty);
                    var path = _imagePath.GetImagePath(fileName, "Clients", vmClient.Name.Replace(" ",string.Empty));
                    using (var stream=new FileStream(path,FileMode.Create))
                    {
                        vmClient.Image.CopyTo(stream);
                    }
                   client.Image= _imagePath.GetImagePathForDb(path);
                    
                }
                _work.Clients.Add(client);
                _work.Save();
                return Json(true);
            }
            return Json(false);
        
        }

        public IActionResult LoadClient()
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
            
           //List<Client> ClientList = _work.Clients.GetAll();

          //  List<vmClient> clientComponent= _work.Clients.GetAllClient();

            List<Client> clientList = _work.Clients.GetAll();
            List<vmClient> clientItem = new List<vmClient>();
            foreach (var item in clientList)
            {
                string photoURL = "";
                if (!string.IsNullOrEmpty(item.Image))
                {
                    photoURL = _imagePath.GetFilePathAsSourceUrl(item.Image);
                }
                else
                {
                    photoURL = _imagePath.GetFilePathAsSourceUrl("/images/Clients/");
                }
                clientItem.Add(new vmClient()
                {
                    Name = item.Name,
                    Email = item.Email,
                    Address = item.Address,
                    CompanyName = item.CompanyName,
                    Country = item.Country,
                    Phone = item.Phone,
                    Id = item.Id,
                    StatusType = item.StatusType,
                    Photo = item.Image,
                });
            }

            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
            {
               // ClientList = ClientList.AsQueryable().OrderBy(sortColumn + "" + sortColumnDir).ToList();
            }
            else
            {
                clientItem = clientItem.OrderByDescending(m => m.Id).ToList();
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                clientItem = clientItem.Where(m => m.CompanyName.Contains(searchValue) || m.Country.Contains(searchValue) || m.Email.Contains(searchValue) || m.Name.Contains(searchValue) || m.Phone.Contains(searchValue) || m.StatusType.Contains(searchValue) || m.Email.Contains(searchValue)).ToList();
            }

            //total number of rows count     
            recordsTotal = clientItem.Count();

            //Paging     
            var data = clientItem.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
        private static string setEnumValue(int Id)
        {
            string[] n = new string[] { "Lost", "Folloup", "Converted", "NewLead", "LeadManager" };

            int count = 0;
            for (int i = 102; i <= 106; i++)
            {
                if (Id == i)
                {
                    return n[count];
                }
                count++;
            }
            return n[4];
        }

       


    }
}
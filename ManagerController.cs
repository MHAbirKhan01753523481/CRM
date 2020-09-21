using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectMaking.Data;
using ProjectMaking.Helper;
using ProjectMaking.Models.ViewModel;
using ProjectMaking.Models.EntityModel;
using ProjectMaking.UnitOfWorks;
using System.IO;

namespace ProjectMaking.Controllers
{
    public class ManagerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _work;
        private readonly IImagePath _imagePath;
        public ManagerController(AppDbContext context,IUnitOfWork work,IImagePath imagePath)
        {
            _context = context;
            _work = work;
            _imagePath = imagePath;
        }
        public IActionResult Index()
        {
            ViewBag.managerItem = _work.Managers.GetAll();

            return View();
        }

        public IActionResult CreateManagerView()
        {
            vmManager manager = new vmManager();
            ViewBag.country = _work.dropdown.GetCountry();
            return PartialView("_ManagerView", manager);
        }

        [HttpGet]
        public IActionResult ManagerEditView(int id)
        {
            var managerobj = _work.Managers.Get(id);
            vmManager manager = new vmManager();
            manager.Name = managerobj.Name;
            manager.PhotoUrl = managerobj.Image;
            //  client.Id = managerobj.Id;
            return PartialView("_ManagerEditView", manager);
        }
        [HttpPost]
        public IActionResult ManagerEdit(vmManager vmM)
        {
            var clientObj = _work.Managers.Get(vmM.Id);
            if (clientObj != null)
            {
                clientObj.Name = vmM.Name;

                if (vmM.Image != null)
                {
                    var fileName = string.IsNullOrEmpty(Path.GetExtension(vmM.Image.FileName)) ? ".jpg" : Path.GetExtension(vmM.Image.FileName);
                    var extn = vmM.Id + fileName;
                    var path = _imagePath.GetImagePath(extn,"ManagerUpload", "Manager");
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        vmM.Image.CopyTo(stream);
                    }
                    clientObj.Image = _imagePath.GetImagePathForDb(path);
                }
                _work.Managers.Update(clientObj);
                _work.Save();
                return PartialView("_ManagerEditView", clientObj);
            }
            return PartialView("_ManagerEditView", vmM);
        }

        public IActionResult ManagerCreate(vmManager vm)
        {

            var itemVm = _work.Managers.GetAll().Count();

            if (ModelState.IsValid)
            {
               
                if (itemVm < 3)
                {
                    Manager manager = new Manager()
                    {

                        Name = vm.Name,
                        Email = vm.Email,
                        Address = vm.Address,
                        Phone = vm.Phone,
                        Country = vm.Country,

                        Bsc = vm.Bsc,
                        HSC = vm.HSC,
                        SSC = vm.SSC,
                        Skill = vm.Skill,
                        Designation = vm.Designation,

                        Facebook = vm.Facebook,
                        Twitter = vm.Twitter,
                        LinkdIn = vm.LinkdIn,
                    };
                    if (vm.Image != null)
                    {
                        var extn = string.IsNullOrEmpty(Path.GetExtension(vm.Image.FileName)) ? ".jpg" : Path.GetExtension(vm.Image.FileName);

                        var fileName = vm.Name + extn;

                        var path = _imagePath.GetImagePath(fileName, "ManagerUpload", "Manager");

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            vm.Image.CopyTo(stream);
                        }
                        manager.Image = _imagePath.GetImagePathForDb(path);
                    }
                    _work.Managers.Add(manager);
                    _work.Save();
                    return PartialView("_ManagerView", manager);
                }
               
            }
            return PartialView("_ManagerView", vm);
        }

        public IActionResult ManagerDetails()
        {
            List<Manager> clients = _work.Managers.GetAll().Where(x => x.Id == 2009).ToList();
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
                    photoURL = _imagePath.GetFilePathAsSourceUrl("/images/Clients/");
                }
                clientitem.Add(new vmManager
                {
                    Id = item.Id,
                    Name = item.Name,
                    PhotoUrl = item.Image,
                    Phone = item.Phone,
                    Email = item.Email,
                    Address = item.Address,
                    Country = item.Country,
                    HSC=item.HSC,
                    SSC=item.SSC,
                    Bsc=item.Bsc,
                    Skill=item.Skill,
                    Designation=item.Designation,
                    Facebook=item.Facebook,
                    Twitter=item.Twitter,
                    LinkdIn=item.LinkdIn,

                });
            }

            ViewBag.leadmanagerItem = clientitem;

            return PartialView("_ManagerDetails");
        }

        public IActionResult EditView(int id)
        {
            ViewBag.country = _work.dropdown.GetCountry();
            var manager = _work.Managers.Get(id);
            vmManager vmManager = new vmManager();
            //vmManager.Id = manager.Id;
            vmManager.Name = manager.Name;
            vmManager.PhotoUrl = manager.Image;
            vmManager.Phone = manager.Phone;
            vmManager.Email = manager.Email;
            vmManager.Address = manager.Address;
            vmManager.Country = manager.Country;
            vmManager.HSC = manager.HSC;
            vmManager.SSC = manager.SSC;
            vmManager.Bsc = manager.Bsc;
            vmManager.Skill = manager.Skill;
            vmManager.Designation = manager.Designation;
            vmManager.Facebook = manager.Facebook;
            vmManager.Twitter = manager.Twitter;
            vmManager.LinkdIn = manager.LinkdIn;
            return PartialView("_EditView", vmManager);
        }
        [HttpPost]
        public IActionResult UpdateManager(vmManager vmManager)
        {
            var editManager = _work.Managers.Get(vmManager.Id);

            if (editManager != null)
            {
                editManager.Name = vmManager.Name;
                editManager.Phone =vmManager.Phone;
                editManager.Email =vmManager.Email;
                editManager.Address =vmManager.Address;
                editManager.Country =vmManager.Country;
                editManager.HSC =vmManager.HSC;
                editManager.SSC =vmManager.SSC;
                editManager.Bsc =vmManager.Bsc;
                editManager.Skill =vmManager.Skill;
                editManager.Designation =vmManager.Designation;
                editManager.Facebook =vmManager.Facebook;
                editManager.Twitter =vmManager.Twitter;
                editManager.LinkdIn =vmManager.LinkdIn;

                if (vmManager.Image !=null)
                {
                    var exten = string.IsNullOrEmpty(Path.GetExtension(vmManager.Image.FileName)) ? ".jpg" : Path.GetExtension(vmManager.Image.FileName);

                    var fileName = vmManager.Name + exten;

                    var path = _imagePath.GetImagePath(fileName, "ManagerUpload", "Manager");

                    using (var stream=new FileStream(path,FileMode.Create))
                    {
                        vmManager.Image.CopyTo(stream);
                    }
                    editManager.Image = _imagePath.GetImagePathForDb(path);
                }
                _work.Managers.Update(editManager);
                _work.Save();
                return PartialView("_EditView", editManager);
            }
            return PartialView("_EditView", vmManager);
        }

        public IActionResult Delete(int id)
        {
            var managerObj = _work.Managers.Get(id);
            vmManager vmManager = new vmManager();
            if (managerObj ==null)
            {
                return NotFound();
            }
            if (managerObj.Id == 2009)
            {
                return Json(false);
            }
            else
            {
                _work.Managers.Remove(managerObj);
                _work.Save();
            }
            return Json(true);
        }
        public IActionResult LoadManager()
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

            List<Manager> managerList = _work.Managers.GetAll();
            List<vmManager> managerItem = new List<vmManager>();
            foreach (var item in managerList)
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
                managerItem.Add(new vmManager()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Address = item.Address,
                    Country = item.Country,
                    Phone = item.Phone,
                    HSC=item.HSC,
                    SSC=item.SSC,
                    Bsc=item.Bsc,
                    Designation=item.Designation,
                    Facebook=item.Facebook,
                    Skill=item.Skill,
                    LinkdIn=item.LinkdIn,
                    Twitter=item.Twitter,
                    PhotoUrl=item.Image,
                });
            }

            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
            {
                // ClientList = ClientList.AsQueryable().OrderBy(sortColumn + "" + sortColumnDir).ToList();
            }
            else
            {
                managerItem = managerItem.OrderByDescending(m => m.Id).ToList();
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
              
                managerItem = managerItem.Where(m => m.Name.Contains(searchValue)).ToList();
            }

            //total number of rows count     
            recordsTotal = managerItem.Count();

            //Paging     
            var data = managerItem.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
    }
}
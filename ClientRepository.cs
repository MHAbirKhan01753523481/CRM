using ProjectMaking.BaseManager;
using ProjectMaking.Data;
using ProjectMaking.Helper;
using ProjectMaking.Models.EntityModel;
using ProjectMaking.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectMaking.Repository
{
    public class ClientRepository:BaseManager<Client>,IClientRepository
    {
        private readonly AppDbContext _context;
  
        //public ClientRepository(AppDbContext context, IImagePath iamgePath) : base(context)
        //{
        //    _context = context;
        //    _imagePath = iamgePath;
        //}
        public ClientRepository(AppDbContext context):base(context)
        {
            _context = context;
           
        }

        public dynamic ActiveLead()
        {
            return _context.Clients.ToList().Where(f => f.StatusType == "103" || f.StatusType == "104" || f.StatusType == "105").Count();
        }

        public List<vmClient> GetAllClient()
        {
            List<Client> clientList = _context.Clients.ToList();
            List<vmClient> clientItem = new List<vmClient>();
            foreach (var item in clientList)
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
                    StatusType=item.StatusType,
                    Photo=item.Image,
                });
            }
            return clientItem;
        }

        public List<Client> LeadManager()
        {
            List<Client> clientList = _context.Clients.Where(x => x.Name == "Anwar" || x.Id==1).ToList();
            List<vmClient> list = new List<vmClient>();
            foreach (var item in clientList)
            {
                list.Add(new vmClient
                {
                    Id=item.Id,
                    Name=item.Name
                });
            }

            return clientList;
        }

        public dynamic LostLead()
        {
            return _context.Clients.Where(x => x.StatusType == "102").Count();
        }

        public dynamic TotalLead()
        {
            return _context.Clients.ToList().Count();
        }
    }
}

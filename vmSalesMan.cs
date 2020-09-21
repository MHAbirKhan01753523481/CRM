using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Models.ViewModel
{
    public class vmSalesMan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [DisplayName("Pass ")]
        public bool Pass { get; set; }
        public IFormFile Image { get; set; }
        [DisplayName("Client Name")]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        [DisplayName("Product Name")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        [DisplayName("Sehedule Type")]
        public int SeheduleId { get; set; }
        public string SeheduleName { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string ChangeDateTime { get; set; }
        [DisplayName("Icon Class")]
        public string IconClass { get; set; }
        [DisplayName("Icon Color")]
        public string IconColor { get; set; }
        public string Comment { get; set; }
        public double Mark { get; set; }
        public string Replay { get; set; }
        [DisplayName("Manager Name")]
        //public int ManagerId { get; set; }
        //public string ManagerName { get; set; }
        public string Photo { get; set; }


    }
}

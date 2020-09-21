using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Models.EntityModel
{
    public class Accounting
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Paybill { get; set; }
        public decimal DeuBill { get; set; }
        public decimal TotalBill { get; set; }
        public DateTime IssuDate { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    
    }
}

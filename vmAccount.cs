using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Models.ViewModel
{
    public class vmAccount
    {
        public int Id { get; set; }
        [DisplayName("Account Name")]
        public string AccountName { get; set; }
        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }
        [DisplayName("Pay Bill")]
        public decimal Paybill { get; set; }
        [DisplayName("Deu Bill")]
        public decimal DeuBill { get; set; }
        [DisplayName("Total Bill")]
        public decimal TotalBill { get; set; }
        [DisplayName("Issu Date")]
        public DateTime IssuDate { get; set; }
        public string ChangeIssuDate { get; set; }
       
        public int ClientName { get; set; }
        [DisplayName("Client Name")]
        public int ClientId { get; set; }
        [DisplayName("Product Name")]
        public int ProductId { get; set; }
      
        public int ProductName { get; set; }
    }
}

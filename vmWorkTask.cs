using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Models.ViewModel
{
    public class vmWorkTask
    {
        public int Id { get; set; }
        [DisplayName("Task Name")]
        public string Name { get; set; }
        public double Mark { get; set; }
        public string Comment { get; set; }
        public string Reply { get; set; }
        public string Requirement { get; set; }
        [DisplayName("To Date")]
        public DateTime ToDate { get; set; }
        public string ChangeToDate { get; set; }

        [DisplayName("Icon Class")]
        public string IconClass { get; set; }
        [DisplayName("Icon Color")]
        public string IconColor { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        public string ChangeEndDate { get; set; }
        [DisplayName("Complite Time")]

        public DateTime CompliteTime { get; set; }
        public string ChangeCompliteTime { get; set; }
       
        public string ManagerName { get; set; }
        [DisplayName("Manager")]
        public int ManagerId { get; set; }
       
        public string SalesManName { get; set; }
        [DisplayName("Sales Man")]
        public int SalesManId { get; set; }
        [DisplayName("Paid Ammount")]
        public decimal PaidAmmount { get; set; }
        [DisplayName("Dew Ammount")]
        public decimal DewAmmount { get; set; }
    }
}

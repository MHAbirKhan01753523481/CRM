using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Models.EntityModel
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Trade Trade { get; set; }
        public int TradeId  { get; set; }
        public string MettingDate { get; set; }
        public string NextMettingDate { get; set; }
    }
}

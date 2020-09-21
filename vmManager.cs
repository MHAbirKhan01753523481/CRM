using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Models.ViewModel
{
    public class vmManager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Designation { get; set; }
        public string Skill { get; set; }
        public string Bsc { get; set; }
        public string HSC { get; set; }
        public string SSC { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkdIn { get; set; }
        public string PhotoUrl { get; set; }
    }
}

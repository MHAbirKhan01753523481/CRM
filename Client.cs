﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Models.EntityModel
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  CompanyName  { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string StatusType { get; set; }
        public string Image { get; set; }
       
    }
}

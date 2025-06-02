﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; } // acts as Username
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Admin / Employee
    }
}
 
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiDAL.Dtos.Response
{
    public class LoginResponseModal
    {
        public string Username { get; set; }
        public string UserId { get; set; }
        public string Email { get; set;  }
        public string Token { get; set; }
    }
}

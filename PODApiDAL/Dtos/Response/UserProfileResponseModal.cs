using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiDAL.Dtos.Response
{
    public class UserProfileResponseModal
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Occupation { get; set; }
        public string? Message { get; set; }
        public string? CompanyName { get; set; }
    }
}

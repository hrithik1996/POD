using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiDAL.Dtos.Request
{
    public class UpdateUserProfile
    {
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string Message { get; set; }
    }
}

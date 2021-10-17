using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PODApiDAL.Common
{
    public class ApplicationResponse
    {
        public bool Status { get; set; }
        public HttpStatusCode StatusCode {  get; set; }
        public string? Message { get;set;  }
        public Object? data { get; set;  }
    }
}

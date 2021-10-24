using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.CommonUtilities
{
    public class ApiRouteInfo
    {
        public const string BasicRoute = "api/[controller]";
        public const string RegisterUser = "RegisterUser";
        public const string LoginUser = "LoginUser";
        public const string GetUserProfile = "GetUserProfile";
        public const string UpdateProfile = "UpdateProfile";

        public const string CreatePost = "CreatePost";
        public const string GetPost = "GetPost";
    }
}

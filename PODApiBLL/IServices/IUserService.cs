using Microsoft.AspNetCore.Identity;
using PODApiDAL.Common;
using PODApiDAL.Dtos;
using PODApiDAL.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiBLL.IServices
{
    public interface IUserService
    {
        Task<ApplicationResponse> RegisterUser(RegisterUserModal registerUser);
        Task<ApplicationResponse> LoginUser(LoginUserModal loginUser);
        Task<ApplicationResponse> GetUserProfile(string userId);
        Task<ApplicationResponse> UpdateProfileAsync(UpdateUserProfile updateUserProfile, string userId);
    }
}

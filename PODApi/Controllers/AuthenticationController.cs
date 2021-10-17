using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PODApiBLL.IServices;
using PODApiDAL.Dtos;
using System;
using System.Threading.Tasks;
using Utilities.CommonUtilities;

namespace PODApi.Controllers;
[Route(ApiRouteInfo.BasicRoute)]
[ApiController]
public class AuthenticationController : ControllerBase
{
    public IUserService user { get;set;  }

    public AuthenticationController(IUserService user)
    {
        this.user = user;
    }

    [HttpPost]
    [Route(ApiRouteInfo.RegisterUser)]
    public async Task<ActionResult> RegisterUser(RegisterUserModal registerUser)
    {
        try
        {
            var registerResult = await user.RegisterUser(registerUser);
            return Ok(registerResult);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route(ApiRouteInfo.LoginUser)]
    public async Task<IActionResult> Login(LoginUserModal login)
    {
        try
        {
            var userLogin = await user.LoginUser(login);
            return Ok(userLogin);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Authorize]
    [Route(ApiRouteInfo.GetUserProfile)]
    public async Task<IActionResult> GetUserProfile(string userId)
    {
        try
        {
            var userProfile = await user.GetUserProfile(userId);
            return Ok(userProfile);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PODApiBLL.IServices;
using PODApiDAL.Common;
using PODApiDAL.DataContext;
using PODApiDAL.Dtos;
using PODApiDAL.Dtos.Request;
using PODApiDAL.Dtos.Response;
using PODApiDAL.Entities;
using Utilities.CommonUtilities;

namespace PODApiBLL.Services
{
    public class UserService : IUserService
    {

        private DatabaseContext databaseContext;
        private ApplicationResponse applicationResponse;
        private UserManager<IdentityUser> userManager { get; set; }
        private RoleManager<IdentityRole> roleManager { get; set; }
        private IConfiguration configuration {  get; set; }


        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="databaseContext"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="configuration"></param>
        public UserService(DatabaseContext databaseContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.databaseContext = databaseContext;
            this.applicationResponse = new ApplicationResponse();
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        /// <summary>
        /// Method for Login User
        /// </summary>
        /// <param name="loginUserModal"></param>
        /// <returns></returns>
        public async Task<ApplicationResponse> LoginUser(LoginUserModal loginUserModal)
        {
            try
            {
                var user = await userManager.FindByNameAsync(loginUserModal.UserName);
                if(user != null && await userManager.CheckPasswordAsync(user, loginUserModal.Password))
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("Id",user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach(var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: configuration["JwtSettings:ValidIssuer"],
                        audience: configuration["JwtSettings:ValidAudience"],
                        expires: DateTime.Now.AddDays(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    applicationResponse.Status = true;
                    applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    applicationResponse.Message = MessagesUtility.Success;
                    applicationResponse.data = new LoginResponseModal
                    {
                        Email = user.Email,
                        UserId = user.Id,
                        Username = user.UserName,
                        Token = new JwtSecurityTokenHandler().WriteToken(token)
                    };
                }
                else
                {
                    applicationResponse.Status = false;
                    applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    applicationResponse.Message = MessagesUtility.UserPasswordIncorrect;
                }
            }
            catch(Exception ex)
            {
                applicationResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                applicationResponse.Message = ex.Message;
                applicationResponse.Status = false;
            }
            return applicationResponse;
        }

        /// <summary>
        /// Method for registering the user
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        public async Task<ApplicationResponse> RegisterUser(RegisterUserModal registerUser)
        {
            try
            {
                var userExists = await userManager.FindByNameAsync(registerUser.Username);
                if (userExists == null)
                {
                    IdentityUser user = new IdentityUser()
                    {
                        Email = registerUser.Email,
                        UserName = registerUser.Username,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        PhoneNumber = registerUser.PhoneNumber
                    };

                    //Create User

                    var createUser = await userManager.CreateAsync(user, registerUser.Password);
                    if (createUser.Succeeded)
                    {
                        if (!await roleManager.RoleExistsAsync(ApplicationUserRoles.Admin))
                            await roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.Admin));

                        if (!await roleManager.RoleExistsAsync(ApplicationUserRoles.User))
                            await roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.User));

                        if(!string.IsNullOrEmpty(registerUser.Role) && registerUser.Role == ApplicationUserRoles.Admin)
                        {
                            await userManager.AddToRoleAsync(user, ApplicationUserRoles.Admin);
                        }
                        else
                        {
                            await userManager.AddToRoleAsync(user, ApplicationUserRoles.User);
                        }
                    }

                    var profile = new UserProfile()
                    {
                        CompanyName= registerUser.CompanyName,
                        Message= registerUser.Message,
                        Name = registerUser.Name,
                        Occupation = registerUser.Occupation,
                        UserId = Guid.Parse(user.Id),
                    };

                    await databaseContext.AddAsync(profile);
                    await databaseContext.SaveChangesAsync();

                    applicationResponse.Status = true;
                    applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    applicationResponse.Message = MessagesUtility.Success;
                    applicationResponse.data = profile;
                }
                else
                {
                    applicationResponse.Status = false;
                    applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    applicationResponse.Message = MessagesUtility.UsernameNotAvailable;
                }
            }
            catch (Exception ex)
            {
                applicationResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                applicationResponse.Message = ex.Message;
                applicationResponse.Status = false;
            }

            return applicationResponse;
        }

        /// <summary>
        /// Method for fetching the user profile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApplicationResponse> GetUserProfile(string userId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);
                var userDetail = databaseContext.UserProfiles.FirstOrDefault(x => x.UserId == Guid.Parse(user.Id));

                if(userDetail != null)
                {
                    applicationResponse.Status = true;
                    applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    applicationResponse.Message = MessagesUtility.Success;
                    applicationResponse.data = new UserProfileResponseModal
                    {
                        CompanyName= userDetail.CompanyName,
                        Email = user.Email,
                        Message = userDetail.Message,
                        Name= userDetail.Name,
                        Occupation = userDetail.Occupation,
                        PhoneNumber = user.PhoneNumber,
                        UserName = user.UserName
                    };
                }
                else
                {
                    applicationResponse.Status = false;
                    applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    applicationResponse.Message = MessagesUtility.NoProfile;
                }
            }
            catch(Exception ex)
            {
                applicationResponse.Status = false;
                applicationResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                applicationResponse.Message = ex.Message;
            }
            return applicationResponse;
        }

        public async Task<ApplicationResponse> UpdateProfileAsync(UpdateUserProfile updateUserProfile, string userId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);
                if(user == null)
                {
                    applicationResponse.Status = false;
                    applicationResponse.Message = MessagesUtility.NoProfile;
                    applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    var profileUpdate = databaseContext.UserProfiles.First(x => x.UserId == Guid.Parse(user.Id));

                    profileUpdate.Name = updateUserProfile.Name;
                    profileUpdate.CompanyName = updateUserProfile.CompanyName;
                    profileUpdate.Message = updateUserProfile.Message;
                    profileUpdate.Occupation = updateUserProfile.Occupation;

                    var result = await databaseContext.SaveChangesAsync();

                    applicationResponse.Status = true;
                    applicationResponse.Message = MessagesUtility.Success;
                    applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                }
            }
            catch(Exception ex)
            {
                applicationResponse.Status = false;
                applicationResponse.Message = ex.Message;
                applicationResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return applicationResponse;
        }
    }
}

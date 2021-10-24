using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PODApiBLL.IServices;
using PODApiDAL.Dtos;
using PODApiDAL.Dtos.Request;
using System;
using System.Threading.Tasks;
using Utilities.CommonUtilities;


namespace PODApi.Controllers
{
    [Route(ApiRouteInfo.BasicRoute)]
    [ApiController]
    public class PostController : ControllerBase
    {
        public IPostService postService { get; set; }
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost]
        [Authorize]
        [Route(ApiRouteInfo.CreatePost)]
        public async Task<IActionResult> CreatePost(PostModal postModal)
        {
            try
            {
                var userId = User.FindFirst("Id").Value;
                var result = await postService.CreatePost(postModal, userId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route(ApiRouteInfo.GetPost)]
        public async Task<IActionResult> GetPost(int? postId)
        {
            try
            {
                var userId = User.FindFirst("Id").Value;
                var result = await postService.GetPost(userId, postId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Route(ApiRouteInfo.UpdateProfile)]
        public async Task<IActionResult> UpdatePost(PostModal postModal)
        {
            try
            {
                var userId = User.FindFirst("Id").Value;
                var result = await postService.UpdatePost(userId, postModal);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PODApiBLL.IServices;
using PODApiDAL.Common;
using PODApiDAL.DataContext;
using PODApiDAL.Dtos.Request;
using PODApiDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.CommonUtilities;

namespace PODApiBLL.Services
{
    public class PostService : IPostService
    {
        private DatabaseContext databaseContext;
        private ApplicationResponse applicationResponse;
        private UserManager<IdentityUser> userManager { get; set; }
        private IConfiguration configuration { get; set; }
        public PostService(DatabaseContext databaseContext, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.databaseContext = databaseContext;
            this.userManager = userManager;
            applicationResponse = new ApplicationResponse();
            this.configuration = configuration;
        }

        #region Implementation of IPostService

        public async Task<ApplicationResponse> CreatePost(PostModal postModal, string createdBy)
        {
            try
            {
                var createPost = new Posts()
                {
                    PostName = postModal.PostName,
                    PostTitle = postModal.PostTitle,
                    PostBody = postModal.PostBody,
                    CreatedBy = createdBy,
                    CreatedUtc = DateTime.UtcNow,
                    IsDeleted = false,
                    MediaUrl = postModal.MediaUrl,
                };

                await databaseContext.AddAsync(createPost);
                await databaseContext.SaveChangesAsync();

                applicationResponse.Status = true;
                applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                applicationResponse.Message = MessagesUtility.PostCreated;

            }
            catch(Exception ex)
            {
                applicationResponse.Status = true;
                applicationResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                applicationResponse.Message = ex.Message;
            }
            return applicationResponse;
        }
        public Task<ApplicationResponse> CreateComment(PostCommentModal postCommentModal)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResponse> DeleteComment(int CommentId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResponse> DeleteLike(int LikeId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResponse> DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResponse> LikePost(int PostId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationResponse> UpdatePost(string userId, PostModal postModal)
        {
            try
            {
                var post = databaseContext.Posts.FirstOrDefault(x => x.PostId == postModal.PostId);
                if(post == null)
                {
                    applicationResponse.Status = false;
                    applicationResponse.Message = MessagesUtility.NoProfile;
                    applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    if(post.CreatedBy == userId)
                    {
                        post.PostName = postModal.PostName;
                        post.PostBody = postModal.PostBody;
                        post.PostTitle = postModal.PostTitle;
                        post.UpdatedUtc = DateTime.UtcNow;
#pragma warning disable CS8601 // Possible null reference assignment.
                        post.MediaUrl = postModal.MediaUrl;
#pragma warning restore CS8601 // Possible null reference assignment.
                        post.IsDeleted = false;

                        await databaseContext.SaveChangesAsync();

                        applicationResponse.Status = true;
                        applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                        applicationResponse.Message = MessagesUtility.Success;
                    }
                    else
                    {
                        applicationResponse.Status = false;
                        applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                        applicationResponse.Message = MessagesUtility.OwnerCanUpdatePost;
                    }
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

        public async Task<ApplicationResponse> GetPost(string? userId, int? postId)
        {
            try
            {
                IEnumerable<Posts> posts = null;
                if(userId != null)
                {
                    var user = await userManager.FindByIdAsync(userId);
                    posts = databaseContext.Posts.Where(x => x.CreatedBy == userId).ToList();
                }
                else
                {
                    posts = databaseContext.Posts.ToList();
                }

                if(posts == null)
                {
                    applicationResponse.Status = false;
                    applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    applicationResponse.Message = MessagesUtility.NoData;
                }
                else
                {
                    applicationResponse.Status = true;
                    applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    applicationResponse.Message = MessagesUtility.PostList;
                    applicationResponse.data = posts;
                }
                
            }
            catch(Exception ex)
            {
                applicationResponse.Status = false;
                applicationResponse.StatusCode = System.Net.HttpStatusCode.OK;
                applicationResponse.Message = ex.Message;
            }
            
            return applicationResponse;
        }

        #endregion
    }
}

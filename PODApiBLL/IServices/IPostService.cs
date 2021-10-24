using PODApiDAL.Common;
using PODApiDAL.Dtos.Request;
using PODApiDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiBLL.IServices
{
    public interface IPostService
    {
        Task<ApplicationResponse> CreatePost(PostModal postModal, string createdBy);
        Task<ApplicationResponse> GetPost(string? userId, int? postId);
        Task<ApplicationResponse> UpdatePost(string userId, PostModal postModal);
        Task<ApplicationResponse> DeletePost(int postId);
        Task<ApplicationResponse> CreateComment(PostCommentModal postCommentModal);
        Task<ApplicationResponse> LikePost(int PostId);
        Task<ApplicationResponse> DeleteComment(int CommentId);
        Task<ApplicationResponse> DeleteLike(int LikeId);
    }
}

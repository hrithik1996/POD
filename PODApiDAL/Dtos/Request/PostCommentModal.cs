using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiDAL.Dtos.Request
{
    public class PostCommentModal
    {
        public int? CommentId { get; set; }
        public int PostId { get; set; }
        public string CommentMessage { get; set; }
        public string CommentedBy { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime UpdatedUtc { get; set; }
        public bool IsDeleted { get; set; }
    }
}

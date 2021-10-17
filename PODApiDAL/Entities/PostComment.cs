using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiDAL.Entities
{
    public class PostComment
    {
        [Key]
        public int Id {  get; set; }
        public int PostId { get; set; }
        public string CommentMessage { get; set; }
        public string CommentedBy { get; set;  }
        public DateTime CreatedUtc { get; set; }
        public DateTime UpdatedUtc { get; set; }
        public bool IsDeleted {  get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiDAL.Entities
{
    public class PostLike
    {
        [Key]
        public int Id {  get; set; }
        public int PostId { get; set; }
        public string LikedBy { get; set; }
        public DateTime CreatedUtc { get; set; }
        public bool IsDeleted {  get; set; }
    }
}

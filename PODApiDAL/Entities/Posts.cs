using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiDAL.Entities
{
    public class Posts
    {
        [Key]
        public int PostId {  get; set; }
        public string PostName {  get; set; }
        public string PostTitle {  get; set; }
        public string PostBody {  get; set; }
        public string MediaUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedUtc {  get; set; }
        public DateTime UpdatedUtc {  get; set; }
        public bool IsDeleted { get; set; }
    }
}

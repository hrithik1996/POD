using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiDAL.Entities
{
    public class UserFollowingMapping
    {
        [Key]
        public int UserFollowingId { get; set; }
        public string UserId {  get; set; }
        public string FollowingId { get; set; }
        public bool IsFollowing {  get; set; }
    }
}

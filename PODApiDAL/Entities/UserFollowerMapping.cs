using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiDAL.Entities
{
    public class UserFollowerMapping
    {
        [Key]
        public int UserFollowerId {  get; set; }
        public string UserId { get; set; }
        public string FollowerId { get; set; }
        public bool IsFollower {  get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiDAL.Entities
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        

        [StringLength(50)]
        public string? Occupation { get; set; }
        
        [StringLength(50)]
        public string? CompanyName { get; set; }
        
        [StringLength(200)]
        public string? Message { get; set; }
    }
}

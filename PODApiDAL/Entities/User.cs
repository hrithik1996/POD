using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PODApiDAL.Entities
{
    public class User
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public string Name {  get; set; }
        [Required]
        public string Email {  get; set; }  
        [Required]
        public string Password {  get; set; }   

    }
}

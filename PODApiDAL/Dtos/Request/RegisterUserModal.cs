using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PODApiDAL.Dtos
{
    public class RegisterUserModal
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Occupation { get; set; }
        public string? CompanyName { get; set; }
        public string? Message { get; set; }
        public string Role { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

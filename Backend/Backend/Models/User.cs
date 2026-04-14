using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;

namespace Backend.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;

    }
}
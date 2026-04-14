using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;

namespace Backend.Models
{
    public class User : IdentityUser
    {

        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;

        public string? FullName => $"{FirstName}{LastName}"; //this will be a concatination of the first and last naem

        public Provider? ProviderProfile { get; set; }
        public Patient? PatientProfile { get; set; }
    }
}
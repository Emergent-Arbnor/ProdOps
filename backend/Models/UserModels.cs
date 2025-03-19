using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class NewUser
    {
        [BindRequired]
        [Required]
        [StringLength(10)]
        public required string Username { get; set; }

        [BindRequired]
        [Required]
        public required string Password { get; set; }

        [BindRequired]
        [Required]
        public required bool IsAdmin { get; set; }

    }

    public class UserCredentials
    {
        [BindRequired]
        [Required]
        [StringLength(10)]
        public required string Username { get; set; }

        [BindRequired]
        [Required]
        public required string Password { get; set; }
    }

    public class UserCredentialsResponse
    {
        [BindRequired]
        [Required]
        public bool DoesExist { get; set; }

        [BindRequired]
        [Required]
        public bool IsAdmin {  get; set; } 
    }
}

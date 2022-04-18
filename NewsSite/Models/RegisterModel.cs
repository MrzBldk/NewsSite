using System.ComponentModel.DataAnnotations;

namespace NewsSite.Models
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "UnmatchingPasswords")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
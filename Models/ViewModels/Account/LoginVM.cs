using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        [UIHint("email")]
        [Display(Name = "Epost")]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }
    }
}

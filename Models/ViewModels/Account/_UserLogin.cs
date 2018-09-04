using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class _UserLogin
    {
        [Required(ErrorMessage = "Epost är obligatoriskt")]
        [EmailAddress]
        [Display(Name = "Epost")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }
    }
}

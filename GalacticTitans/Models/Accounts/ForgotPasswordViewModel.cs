using System.ComponentModel.DataAnnotations;

namespace GalacticTitans.Models.Accounts
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

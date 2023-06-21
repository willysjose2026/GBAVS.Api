using System.ComponentModel.DataAnnotations;

namespace ForgotPasswordUI.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}

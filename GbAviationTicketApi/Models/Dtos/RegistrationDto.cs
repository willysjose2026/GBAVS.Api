using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GbAviationTicketApi.Extentions;
using GbAviationTicketApi.Common;

namespace GbAviationTicketApi.Models.Dtos
{
    public class RegistrationDto : BaseDto, IValidable
    {
        public RegistrationDto()
        {

        }

        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(PASS_REGEX,ErrorMessage = PASSWORD_ERROR_MESSAGE)]
        public string Password { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string FullName { get; set; } = null!;

        [Required]
        public int TerminalId { get; set; }

        [Required]
        public string Role { get; set; } = null!;

        public override void Normalize()
        {
            Username = Username.Trim().ToLower();
            FullName = FullName.ToCapitalize();
            Role = Role.ToUpper();
        }
    }
}

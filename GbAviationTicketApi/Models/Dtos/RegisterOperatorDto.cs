using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GbAviationTicketApi.Extentions;
using GbAviationTicketApi.Common;

namespace GbAviationTicketApi.Models.Dtos
{
    public class RegisterOperatorDto : BaseDto, IValidable
    {

        public RegisterOperatorDto()
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

        public override void Normalize()
        {
            Username = Username.Trim().ToLower();
            FullName = FullName.ToCapitalize();
        }
    }
}

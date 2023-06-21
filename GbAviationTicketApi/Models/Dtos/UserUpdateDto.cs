using GbAviationTicketApi.Common;
using System.ComponentModel.DataAnnotations;

namespace GbAviationTicketApi.Models.Dtos
{
    public class UserUpdateDto : BaseDto, IValidable
    {
        public UserUpdateDto()
            : base()
        {

        }

        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(PASS_REGEX, ErrorMessage = PASSWORD_ERROR_MESSAGE)]
        public string Password { get; set; } = "";

        [Required(AllowEmptyStrings = false)]
        public string FullName { get; set; } = null!;

        [Required]
        public int TerminalId { get; set; }

        public override void Normalize()
        {
        }
    }
}

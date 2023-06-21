using GbAviationTicketApi.Common;
using System.ComponentModel.DataAnnotations;

namespace GbAviationTicketApi.Models.Dtos
{
    public class LoginRequestDto : BaseDto, IValidable
    {
        public LoginRequestDto()
            : base()
        {
            
        }

        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MinLength(8)]
        public string Password { get; set; } = null!;

        public override void Normalize()
        {
            Username = Username.Trim().ToLower();
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace GbAviationTicketApi.Models.Dtos
{
    public class LoginResponseDto : BaseDto
    {
        public LoginResponseDto()
        {

        }


        public UserDto? User { get; set; }

        public string Token { get; set; } = null!;
        public override void Normalize()
        { }
    }
}

using System.ComponentModel.DataAnnotations;

namespace GbAviationTicketApi.Models.Dtos
{
    public class UserDto : BaseDto
    {

        public UserDto() : base() { }


        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MinLength(8)]
        public string Password { get; set; } = "";

        [Required(AllowEmptyStrings = false)]
        public string FullName { get; set; } = null!;

        [Required]
        public int Terminal { get; set; }

        public override void Normalize()
        {
        }
    }
}
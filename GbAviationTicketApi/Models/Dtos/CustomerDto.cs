namespace GbAviationTicketApi.Models.Dtos
{
    public class CustomerDto : BaseDto
    {
        public CustomerDto()
            : base()
        {

        }

        public string Id { get; set; } = null!;

        public string CustomerName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public override void Normalize()
        {

        }
    }
}

namespace GbAviationTicketApi.Models.Dtos
{
    public class ProductDto : BaseDto
    {
        public ProductDto()
            : base()
        {

        }

        public string Id { get; set; } = null!;

        public string? ProductName { get; set; }
        public override void Normalize()
        {
        }
    }
}

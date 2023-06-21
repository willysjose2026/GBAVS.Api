namespace GbAviationTicketApi.Models.Dtos
{
    public class PaymentmthdDto : BaseDto
    {
        public PaymentmthdDto()
            : base()
        {

        }

        public int Id { get; set; }

        public string PmtdName { get; set; } = null!;

        public override void Normalize()
        {
        }
    }
}

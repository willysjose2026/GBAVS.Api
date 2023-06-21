namespace GbAviationTicketApi.Models.Dtos
{
    public class TerminalDto : BaseDto
    {
        public TerminalDto()
            : base()
        {

        }

        public int Id { get; set; }

        public string TerminalName { get; set; } = null!;

        public override void Normalize()
        {
        }
    }
}

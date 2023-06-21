using GbAviationTicketApi.Common;
using System.ComponentModel.DataAnnotations;

namespace GbAviationTicketApi.Models.Dtos
{
    public class ReportRequestDto : BaseDto, IValidable
    {
        public ReportRequestDto()
        {

        }
        [Required(AllowEmptyStrings = false)]
        public string AgentUserName { get; set; } = null!;

        public string? OperatorUserName { get; set; }

        [Required]
        public int TerminalId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public override void Normalize()
        {
            OperatorUserName = OperatorUserName?.Trim().ToLower();
        }
    }
}

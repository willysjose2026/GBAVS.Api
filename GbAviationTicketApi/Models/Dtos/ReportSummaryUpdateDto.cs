using GbAviationTicketApi.Common;
using System.ComponentModel.DataAnnotations;

namespace GbAviationTicketApi.Models.Dtos
{
    public class ReportSummaryUpdateDto : BaseDto, IValidable
    {
        public ReportSummaryUpdateDto()
            : base()
        {

        }


        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(DOMAIN_REGEX, ErrorMessage = "username must be of valid domain")]
        public string AgentUserName { get; set; } = null!;

        [RegularExpression(DOMAIN_REGEX, ErrorMessage = "username must be of valid domain")]
        public string? OperatorUserName { get; set; }

        [Required]
        public int TerminalId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public override void Normalize()
        {
            AgentUserName = AgentUserName.Trim().ToLower();
            OperatorUserName = OperatorUserName?.Trim().ToLower();
        }
    }
}

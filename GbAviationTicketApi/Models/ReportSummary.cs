using GbAviationTicketApi.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GbAviationTicketApi.Models
{
    [Table("TICKETS_REPORT")]
    [Index(nameof(AgentUserName), nameof(OperatorUserName), nameof(TerminalId), 
        nameof(StartDate), nameof(EndDate),
        Name = "UNIQUE_AGENT_AND_OP_AND_DATES_AND_TERMINALS", IsUnique = true)]
    public class ReportSummary : ModelBase
    {
        public ReportSummary()
            : base()
        {
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        
        public string AgentUserName { get; set; } = null!;

        public string? OperatorUserName { get; set; } = null!;

        [Required]
        public int TerminalId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [ForeignKey(nameof(AgentUserName))]
        public virtual GbavsUser Agent { get; set; } = null!;

        
        [ForeignKey(nameof(OperatorUserName))]
        public virtual GbavsUser Operator { get; set; } = null!;
        

        [ForeignKey(nameof(TerminalId))]
        public virtual Terminal Terminal { get; set; } = null!;
    }
}

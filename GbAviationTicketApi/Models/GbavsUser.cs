using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GbAviationTicketApi.Models
{
    public class GbavsUser : IdentityUser
    {
        public GbavsUser() : base()
        {
            IsActive = true;
            CreatedAt = DateTime.Now;
            ModifiedAt = null;
        }

        [StringLength(50)]
        [Unicode(false)]
        public string FullName { get; set; } = null!;

        public int TerminalId { get; set; }

        public string RoleId { get; set; } = null!;

        [Required]
        public bool? IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        [ForeignKey(nameof(TerminalId))]
        [InverseProperty("Users")]
        public virtual Terminal? Terminal { get; set; }
        
        [InverseProperty(nameof(Ticket.Operator))]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        [ForeignKey(nameof(RoleId))]
        public virtual IdentityRole Role { get; set; } = null!;

    }
}

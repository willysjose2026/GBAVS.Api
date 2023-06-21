using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GbAviationTicketApi.Models;

[Table("TERMINALS")]
[Index("TerminalName", Name = "UQ__TERMINAL__71A915CC35D06BB8", IsUnique = true)]
public partial class Terminal : ModelBase
{

    public Terminal()
        : base()
    {

    }

    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("TERMINAL_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string TerminalName { get; set; } = null!;

    [Column("TERMINAL_LOC")]
    [StringLength(100)]
    [Unicode(false)]
    public string TerminalLoc { get; set; } = null!;

    [InverseProperty("Terminal")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();


    [InverseProperty(nameof(GbavsUser.Terminal))]
    public virtual ICollection<GbavsUser> Users { get; set; } = new List<GbavsUser>();
}

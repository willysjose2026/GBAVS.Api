using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GbAviationTicketApi.Models;

[Table("PAYMENTMTHD")]
public partial class Paymentmthd : ModelBase
{

    public Paymentmthd() : base()
    {

    }

    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("PMTD_NAME")]
    [StringLength(25)]
    [Unicode(false)]
    public string PmtdName { get; set; } = null!;

    [InverseProperty("PaymentMthd")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

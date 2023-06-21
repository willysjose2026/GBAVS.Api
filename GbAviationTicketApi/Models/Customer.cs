using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GbAviationTicketApi.Models;

[Table("CUSTOMERS")]
public partial class Customer : ModelBase
{
    public Customer() : base()
    {
    }

    [Key]
    [Column("ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [Column("CUSTOMER_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string CustomerName { get; set; } = null!;

    [Column("EMAIL")]
    [StringLength(75)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GbAviationTicketApi.Models;

[Table("PRODUCTS")]
[Index("ProductName", Name = "UQ__PRODUCTS__6FDD6CA311B83EC2", IsUnique = true)]
public partial class Product : ModelBase
{
    public Product() : base()
    {

    }

    [Key]
    [Column("ID")]
    [StringLength(25)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [Column("PRODUCT_NAME")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ProductName { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

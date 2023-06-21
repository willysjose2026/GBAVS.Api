using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GbAviationTicketApi.Models.Attributes;
using Microsoft.EntityFrameworkCore;

namespace GbAviationTicketApi.Models;

[Table("TICKETS")]
[Index("TicketNo", Name = "UQ__TICKETS__7F5E594C4B78BCDC", IsUnique = true)]
public partial class Ticket : ModelBase
{
    public Ticket() : base()
    {
    }

    [Column("TICKET_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string TicketNo { get; set; } = null!;

    [Column("CUSTOMER_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string CustomerId { get; set; } = null!;

    [Column("PRODUCT_ID")]
    [StringLength(25)]
    [Unicode(false)]
    public string ProductId { get; set; } = null!;

    [Column("TERMINAL_ID")]
    public int TerminalId { get; set; }

    [Column("PAYMENT_MTHD_ID")]
    public int PaymentMthdId { get; set; }

    [Column("OPERATOR_USERNAME")]
    [StringLength(256)]
    public string OpUserName { get; set; } = null!;

    [Column("ORDER_DATE")]
    public DateTime OrderDate { get; set; }

    [Column("PRODUCT_QTY", TypeName = "decimal(38, 4)")]
    public decimal ProductQty { get; set; }

    [Column("AIRCRAFT_TYPE")]
    [StringLength(15)]
    [Unicode(false)]
    public string AircraftType { get; set; } = null!;

    [Column("TAIL_NO")]
    [StringLength(15)]
    [Unicode(false)]
    public string TailNo { get; set; } = null!;

    [Column("FLIGHT_NO")]
    [StringLength(15)]
    [Unicode(false)]
    public string FlightNo { get; set; } = null!;

    [Column("F_FROM")]
    [StringLength(15)]
    [Unicode(false)]
    public string FFrom { get; set; } = null!;

    [Column("F_TO")]
    [StringLength(15)]
    [Unicode(false)]
    public string FTo { get; set; } = null!;

    [Column("UNIT_NO")]
    [StringLength(15)]
    [Unicode(false)]
    public string UnitNo { get; set; } = null!;

    [Column("INIT_TIME")]
    [Required]
    public TimeSpan InitTime { get; set; }

    [Column("END_TIME")]
    [Required]
    public TimeSpan EndTime { get; set; }

    [Column("TEMPERATURE", TypeName = "decimal(10, 2)")]
    public decimal Temperature { get; set; }

    [Column("IS_CLEAR_AND_BRIGHT")]
    public bool IsClearAndBright { get; set; }

    [Column("IS_WATER_FREE")]
    public bool IsWaterFree { get; set; }

    [Column("IS_PARTICLE_FREE")]
    public bool IsParticleFree { get; set; }

    [Column("API_DENSITY")]
    public int? ApiDensity { get; set; }

    [Column("PIT_NO")]
    public int? PitNo { get; set; }

    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Tickets")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey(nameof(OpUserName))]
    [InverseProperty(nameof(GbavsUser.Tickets))]
    public virtual GbavsUser Operator { get; set; } = null!;

    [ForeignKey("PaymentMthdId")]
    [InverseProperty("Tickets")]
    public virtual Paymentmthd PaymentMthd { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Tickets")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("TerminalId")]
    [InverseProperty("Tickets")]
    public virtual Terminal Terminal { get; set; } = null!;
}

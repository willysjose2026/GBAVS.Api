using GbAviationTicketApi.Common;
using GbAviationTicketApi.Models.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GbAviationTicketApi.Models.Dtos
{
    public class TicketCreateDto : BaseDto, IValidable
    {
        public TicketCreateDto()
            : base()
        {
        }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Ticket No should only have numeric values")]
        public string TicketNo { get; set; } = null!;


        [Required(AllowEmptyStrings = false)]
        public string CustomerId { get; set; } = null!;


        [Required(AllowEmptyStrings = false)]
        public string ProductId { get; set; } = null!;


        [Required]
        public int TerminalId { get; set; }


        [Required]
        public int PaymentMthdId { get; set; }


        [Required(AllowEmptyStrings = false)]
        public string OpUserName { get; set; } = null!;

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public decimal ProductQty { get; set; }

        
        [Required(AllowEmptyStrings = false)]
        public string AircraftType { get; set; } = null!;

        
        [Required(AllowEmptyStrings = false)]
        public string TailNo { get; set; } = null!;

        
        [Required(AllowEmptyStrings = false)]
        public string FlightNo { get; set; } = null!;

        
        [Required(AllowEmptyStrings = false)]
        public string FFrom { get; set; } = null!;

        
        [Required(AllowEmptyStrings = false)]
        public string FTo { get; set; } = null!;

        
        [Required(AllowEmptyStrings = false)]
        public string UnitNo { get; set; } = null!;

        
        [Required(AllowEmptyStrings = false)]
        [TimeSpanFormat(ErrorMessage = $"time format of {nameof(InitTime)} must be of type HH:MM or HH:MM:SS")]
        public string InitTime { get; set; } = null!;

        
        [Required(AllowEmptyStrings = false)]
        [TimeSpanFormat(ErrorMessage = $"time format of {nameof(EndTime)} must be of type HH:MM or HH:MM:SS")]
        public string EndTime { get; set; } = null!;

        [Required]
        public decimal Temperature { get; set; }

        [Required]
        public bool IsClearAndBright { get; set; }

        [Required]
        public bool IsWaterFree { get; set; }

        [Required]
        public bool IsParticleFree { get; set; }

        public int? ApiDensity { get; set; }

        public int? PitNo { get; set; }
        public override void Normalize()
        {
            CustomerId = CustomerId.ToUpper().Trim();
            ProductId = ProductId.ToUpper().Trim();
            ProductQty = decimal.Round(ProductQty, 2);
            OpUserName = OpUserName.Trim().ToLower();
            AircraftType = AircraftType.ToUpper().Trim();
            TailNo = TailNo.ToUpper().Trim();
            FlightNo = FlightNo.ToUpper().Trim();
            FFrom = FFrom.ToUpper().Trim();
            FTo = FTo.ToUpper().Trim();
            UnitNo = UnitNo.ToUpper().Trim();
            Temperature = decimal.Round(Temperature, 2);
        }
    }
}

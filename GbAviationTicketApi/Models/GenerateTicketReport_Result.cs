
namespace GbAviationTicketApi.Models
{
    public class GenerateTicketReport_Result
    {
        public GenerateTicketReport_Result()
        {

        }
        
        public string TICKET_NO { get; set; } = null!;
        public string CUSTOMER_NAME { get; set; } = null!;
        public string PRODUCT_ID { get; set; } = null!;
        public int TERMINAL_ID { get; set; }
        public string PMTD_NAME { get; set; } = null!;
        public string FULL_NAME { get; set; } = null!;
        public DateTime ORDER_DATE { get; set; }
        public decimal PRODUCT_QTY { get; set; }
        public string AIRCRAFT_TYPE { get; set; } = null!;
        public string TAIL_NO { get; set; } = null!;
        public string FLIGHT_NO { get; set; } = null!;
        public string F_FROM { get; set; } = null!;
        public string F_TO { get; set; } = null!;
        public string UNIT_NO { get; set; } = null!;
        public TimeSpan INIT_TIME { get; set; }
        public TimeSpan END_TIME { get; set; }
        public decimal TEMPERATURE { get; set; }
        public bool IS_CLEAR_AND_BRIGHT { get; set; }
        public bool IS_WATER_FREE { get; set; }
        public bool IS_PARTICLE_FREE { get; set; }
        public int API_DENSITY { get; set; }
        public int PIT_NO { get; set; }
    }


}

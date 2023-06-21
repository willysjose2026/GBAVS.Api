using GbAviationTicketApi.Data.IData;
using GbAviationTicketApi.Models;


namespace GbAviationTicketApi.Repository
{
    public class PaymentmthdRepository : RepositoryBase<Paymentmthd>
    {

        public PaymentmthdRepository(IGbavsContext db) : base(db)
        {
        }

        public override Task<Paymentmthd?> UpdateAsync(Paymentmthd entity)
        => null!;
    }
}

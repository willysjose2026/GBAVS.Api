using GbAviationTicketApi.Models;

namespace GbAviationTicketApi.Repository.IRepository
{
    public interface IRepositoryWrapper
    {
        IUserRepository Users { get; }
        ITicketReportRepository TicketReports { get; }
        ICustomerRepository Customers { get; }
        IRepositoryBase<Terminal> Terminals { get; }
        IRepositoryBase<Product> Products { get; }
        IRepositoryBase<Ticket> Tickets { get; }
        IRepositoryBase<Paymentmthd> Paymentmthds { get; }
    }
}

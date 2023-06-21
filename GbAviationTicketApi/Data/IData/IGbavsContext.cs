using GbAviationTicketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GbAviationTicketApi.Data.IData
{
    public interface IGbavsContext
    {
        DbSet<GenerateTicketReport_Result> GenerateTicketReport_Results { get; set; }
        DbSet<ReportSummary> TicketsReportSummaries { get; set; }
        DbSet<GbavsUser> GbavsUsers { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Paymentmthd> Paymentmthds { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Terminal> Terminals { get; set; }
        DbSet<Ticket> Tickets { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task SaveAsync();

    }
}

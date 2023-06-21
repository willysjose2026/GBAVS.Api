using GbAviationTicketApi.Data.IData;
using GbAviationTicketApi.Models;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GbAviationTicketApi.Repository
{
    public class TicketsReportRepository : RepositoryBase<ReportSummary>, ITicketReportRepository
    {
        
        public TicketsReportRepository(IGbavsContext db)
            : base(db)
        {
        }

        public async Task<ReportSummary> CreateReportSummary(ReportSummary ticketSummary)
        {
            await _db.TicketsReportSummaries.AddAsync(ticketSummary);
            await _db.SaveAsync();
            return ticketSummary;
        }

        public async Task<List<GenerateTicketReport_Result>> GenerateTicketReport(ReportSummary details)
        {
            string query = $"CreateTicketReport @StartDate, @EndDate";
            List<SqlParameter> sqlParameters = new()
            {
                new("StartDate", details.StartDate),
                new("EndDate", details.EndDate)
            };

            if (details.OperatorUserName != null)
            {
                sqlParameters.Add(new("Operator", details.OperatorUserName));
                query += ", @Operator";
            } else
            {
                sqlParameters.Add(new("Operator", DBNull.Value));
                query += ", @Operator";
            }

            sqlParameters.Add(new("Terminal", details.TerminalId));
            query += ", @Terminal";

            return await _db.GenerateTicketReport_Results
                .FromSqlRaw(query, sqlParameters.ToArray()).ToListAsync();
        }

        public async Task<List<ReportSummary>> GetReportsSummaries() => 
            await _db.TicketsReportSummaries.ToListAsync();

        public override Task<ReportSummary?> UpdateAsync(ReportSummary entity)
        {
            throw new NotImplementedException();
        }
    }
}

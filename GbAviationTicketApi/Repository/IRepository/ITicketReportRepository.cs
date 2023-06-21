using GbAviationTicketApi.Models;

namespace GbAviationTicketApi.Repository.IRepository
{
    public interface ITicketReportRepository : IRepositoryBase<ReportSummary>
    {
        Task<List<ReportSummary>> GetReportsSummaries();
        Task<ReportSummary> CreateReportSummary(ReportSummary ticket);
        Task<List<GenerateTicketReport_Result>> GenerateTicketReport(ReportSummary details);
    }
}

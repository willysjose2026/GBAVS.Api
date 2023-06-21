using GbAviationTicketApi.Models;

namespace GbAviationTicketApi.Repository.IRepository
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<List<Customer>> GetAllCustomers();
    }
}

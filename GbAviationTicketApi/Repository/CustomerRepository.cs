using GbAviationTicketApi.Data.IData;
using GbAviationTicketApi.Models;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GbAviationTicketApi.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(IGbavsContext db)
            : base(db)
        {
        }

        public async Task<List<Customer>> GetAllCustomers() 
            => await _db.Customers.FromSql($"GetAllActiveCustomers").ToListAsync(); 

        public override Task<Customer?> UpdateAsync(Customer entity) => null!;
    }
}

using GbAviationTicketApi.Data.IData;
using GbAviationTicketApi.Models;

namespace GbAviationTicketApi.Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(IGbavsContext db) : base(db)
        {
        }

        public override Task<Product?> UpdateAsync(Product entity)
        => null!;
    }
}

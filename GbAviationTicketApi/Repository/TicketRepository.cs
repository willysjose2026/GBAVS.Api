using GbAviationTicketApi.Data;
using GbAviationTicketApi.Data.IData;
using GbAviationTicketApi.Models;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Sockets;

namespace GbAviationTicketApi.Repository
{
    public class TicketRepository : RepositoryBase<Ticket>
    {
        public TicketRepository(IGbavsContext db) : base(db)
        {
        }

        public override async Task<Ticket?> UpdateAsync(Ticket entity)
        {
            var ticket = (await FindByConditionAsync(t => t.TicketNo == entity.TicketNo)).FirstOrDefault();
            if (ticket == null)
                return null;

            entity.Id = ticket.Id;
            _ = await SimpleUpdateAsync(entity);
            return entity;
        }
    }
}

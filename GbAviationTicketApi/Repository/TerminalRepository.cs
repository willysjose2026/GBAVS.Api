using GbAviationTicketApi.Data.IData;
using GbAviationTicketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GbAviationTicketApi.Repository
{
    //Hazte una clase abstracta para esa vaina
    public class TerminalRepository : RepositoryBase<Terminal>
    {
        public TerminalRepository(IGbavsContext db) : base(db)
        {
        }

        public Task<bool> TerminalExists(int terminalId)
        {
            return _db.Terminals.AnyAsync(t => t.Id == terminalId && t.IsActive == true);
        }

        public override Task<Terminal?> UpdateAsync(Terminal entity)
        => null!;
    }
}

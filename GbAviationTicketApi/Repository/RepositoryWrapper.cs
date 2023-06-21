using AutoMapper;
using GbAviationTicketApi.Data.IData;
using GbAviationTicketApi.Models;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.AspNetCore.Identity;

namespace GbAviationTicketApi.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IGbavsContext _db;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<GbavsUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private ICustomerRepository _customers = null!;
        private IRepositoryBase<Terminal> _terminals = null!;
        private IUserRepository _users = null!;
        private IRepositoryBase<Product> _products = null!;
        private IRepositoryBase<Paymentmthd> _paymentmthds = null!;
        private IRepositoryBase<Ticket> _tickets = null!;
        private ITicketReportRepository _ticketReport = null!;

        public RepositoryWrapper(IGbavsContext db, IConfiguration config, IMapper mapper,
            UserManager<GbavsUser> userManager, RoleManager<IdentityRole> roleManager )
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _config = config;
        }

        public ICustomerRepository Customers
        {
            get
            {
                _customers ??= new CustomerRepository(_db);
                return _customers;
            }
        }

        public IRepositoryBase<Terminal> Terminals
        {
            get
            {
                _terminals ??= new TerminalRepository(_db);
                return _terminals;
            }
        }

        public IRepositoryBase<Product> Products
        {
            get
            {
                _products ??= new ProductRepository(_db);
                return _products;
            }
        }

        public IRepositoryBase<Ticket> Tickets
        {
            get
            {
                _tickets ??= new TicketRepository(_db);
                return _tickets;
            }
        }

        public IRepositoryBase<Paymentmthd> Paymentmthds
        {
            get
            {
                _paymentmthds ??= new PaymentmthdRepository(_db);
                return _paymentmthds;
            }
        }


        public IUserRepository Users
        {
            get
            {
                _users ??= new UserRepository(_db,_mapper, _config, _userManager, _roleManager);
                return _users;
            }
        }

        public ITicketReportRepository TicketReports
        {
            get
            {
                _ticketReport ??= new TicketsReportRepository(_db);
                return _ticketReport;
            }
        }
    }
}

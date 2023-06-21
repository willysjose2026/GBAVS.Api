using AutoMapper;
using GbAviationTicketApi.Data.IData;
using GbAviationTicketApi.Models;
using GbAviationTicketApi.Models.Dtos;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace GbAviationTicketApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string secretkey;
        private readonly UserManager<GbavsUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IGbavsContext _db;
        private readonly IMapper _mapper;

        public UserRepository(IGbavsContext db, IMapper mapper,
            IConfiguration configuration, UserManager<GbavsUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            secretkey = configuration.GetValue<string>("ApiSettings:Secret") ?? "";
            _db = db;
            _mapper = mapper;
        }

        public async Task DeleteAsync(string username)
        {
            var userToDelete = (await FindByConditionAsync(u => u.UserName == username.ToLower()))
                .FirstOrDefault();

            if (userToDelete != null)
            {
                userToDelete.IsActive = false;
                await UpdateAsync(userToDelete);
            }
        }

        public Task<IQueryable<GbavsUser>> FindAllAsync()
        {
            IQueryable<GbavsUser> query = _db.Set<GbavsUser>();
            return Task.FromResult(query.AsNoTracking().Where(e => e.IsActive == true));
        }

        public Task<IQueryable<GbavsUser>> FindByConditionAsync(Expression<Func<GbavsUser, bool>> filter)
        {
            IQueryable<GbavsUser> query = _db.Set<GbavsUser>();
            return Task.FromResult(query.AsNoTracking().Where(e => e.IsActive == true).Where(filter));
        }

        public async Task<bool> IsUniqueUserAsync(string username)
        {
            var tempuser = (await FindByConditionAsync(u => (u.UserName ?? "").ToLower().Trim() == username))
                .FirstOrDefault();
            return tempuser == null;
        }
        
        public async Task<LoginResponseDto?> Login(LoginRequestDto request)
        {

            var user = (await FindByConditionAsync(u => (u.UserName ?? "") == request.Username)).FirstOrDefault();
            var isValid = await _userManager.CheckPasswordAsync(user ?? new(), request.Password);

            if (user == null || !isValid)
                return new LoginResponseDto() { User = null, Token = "" };

            var roles = await _userManager.GetRolesAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretkey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.UserName?.ToString() ?? ""),
                        new Claim(ClaimTypes.Role, roles.FirstOrDefault()?.ToString() ?? ""),
                        new Claim("Terminal", user.TerminalId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return new()
            {
                Token = tokenHandler.WriteToken(token),
                User = new()
                {
                    UserName = user.UserName ?? "",
                    Password = "",
                    FullName = user.FullName,
                    Terminal = user.TerminalId
                }
            };

        }

        public async Task<UserDto?> Register(RegistrationDto registrationDto)
        {

            if (await IsUniqueUserAsync(registrationDto.Username))
            {
                GbavsUser user = new()
                {
                    UserName = registrationDto.Username,
                    FullName = registrationDto.FullName,
                    TerminalId = registrationDto.TerminalId
                };

                try
                {
                    var role = await _roleManager.FindByNameAsync(registrationDto.Role);
                    if (role != null)
                        user.RoleId = role.Id;
                    var result = await _userManager.CreateAsync(user, registrationDto.Password);
                    if (result.Succeeded)
                    {
                        if (!_roleManager.RoleExistsAsync("ADMIN").GetAwaiter().GetResult())
                        {
                            await _roleManager.CreateAsync(new IdentityRole("ADMIN"));
                            await _roleManager.CreateAsync(new IdentityRole("OPERATOR"));
                            await _roleManager.CreateAsync(new IdentityRole("ARP_AGENT"));
                        }

                        await _userManager.AddToRoleAsync(user, registrationDto.Role);
                        await _userManager.SetEmailAsync(user, user.UserName);
                        var userToReturn = _db.GbavsUsers.FirstOrDefault(u => u.UserName == user.UserName);

                        return new UserDto()
                        {
                            UserName = user.UserName,
                            Password = "",
                            FullName = user.FullName,
                            Terminal = user.TerminalId
                        };
                    }
                }
                catch (DbUpdateException)
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<UserDto?> UpdateAsync(GbavsUser user)
        {
            var userToUpdate = (await FindByConditionAsync(u => u.UserName == user.UserName))
                .FirstOrDefault();

            if (userToUpdate == null)
                return null;

            userToUpdate = _mapper.Map<GbavsUser>(user);
            userToUpdate.ModifiedAt = DateTime.Now;
            _db.GbavsUsers.Update(userToUpdate);
            await _db.SaveAsync();

            return _mapper.Map<UserDto>(userToUpdate);
        }
    }
}

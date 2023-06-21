using GbAviationTicketApi.Models;
using GbAviationTicketApi.Models.Dtos;
using System.Linq.Expressions;

namespace GbAviationTicketApi.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IQueryable<GbavsUser>> FindAllAsync();
        Task<IQueryable<GbavsUser>> FindByConditionAsync(Expression<Func<GbavsUser, bool>> filter);
        Task<bool> IsUniqueUserAsync(string username);
        Task DeleteAsync(string username);
        Task<UserDto?> UpdateAsync(GbavsUser user);
        Task<LoginResponseDto?> Login(LoginRequestDto request);
        Task<UserDto?> Register(RegistrationDto registrationDto);
    }
}

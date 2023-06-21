using AutoMapper;
using GbAviationTicketApi.Models.Dtos;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GbAviationTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<UserController>
    {
        public UserController(IRepositoryWrapper repository, IMapper mapper) : base(repository, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers()
        {
            var operators = (await _repository.Users.FindAllAsync()).ToList();
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = _mapper.Map<List<UserDto>>(operators);
            return Ok(apiResponse);
        }

        [HttpGet(nameof(GetCurrentUserTerminal))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsersByCurrentTerminal()
        {
            var terminal = GetCurrentUserTerminal();
            var operators = (await _repository.Users.FindByConditionAsync(u => u.TerminalId == terminal)).ToList();
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = _mapper.Map<List<UserDto>>(operators);
            return Ok(apiResponse);
        }

        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            var _operator = (await _repository.Users
                .FindByConditionAsync(u => u.UserName == username.ToLower())).FirstOrDefault();

            if (_operator == null)
                return FailResponse(HttpStatusCode.NotFound, $"Operator with username {username} not found");

            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = _operator;
            return Ok(apiResponse);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserTerminal(int newTerminal, string username)
        {
            if (newTerminal == 0 || username == null)
                return FailResponse(null, "username and terminal field are required");

            var terminal = (await _repository.Terminals.FindByConditionAsync(t => t.Id == newTerminal)).FirstOrDefault();
            if (terminal == null)
                return FailResponse(null, "terminal entered is invalid");

            var userToUpdate = (await _repository.Users
                .FindByConditionAsync(u => u.UserName == username.ToLower())).FirstOrDefault();

            if (userToUpdate == null)
                return FailResponse(HttpStatusCode.NotFound, $"user {username} does not exists");

            if (GetCurrentUserName() == username.ToLower())
                return FailResponse(null, "unauthorized operation");

            if (newTerminal == userToUpdate.TerminalId)
                return FailResponse(null, "User is already assigned to this terminal");

            userToUpdate.TerminalId = newTerminal;
            var result = await _repository.Users.UpdateAsync(userToUpdate);

            if (result == null)
                return FailResponse(HttpStatusCode.NotFound, $"user {username} does not exists");

            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = result;
            return Ok(apiResponse);
        }

        [HttpDelete("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(string username)
        {
            if (string.IsNullOrEmpty(username))
                return FailResponse(null, "field username is required");

            var user = (await _repository.Users.FindByConditionAsync(u => u.UserName == username.ToLower())).FirstOrDefault();
            
            if (user == null)
                return FailResponse(HttpStatusCode.NotFound, $"user with username {username} does not exists");

            await _repository.Users.DeleteAsync(username.ToLower());
            apiResponse.StatusCode = HttpStatusCode.OK;
            return Ok(apiResponse);
        }
    }
}

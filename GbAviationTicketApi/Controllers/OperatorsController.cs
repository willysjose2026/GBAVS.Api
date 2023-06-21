using AutoMapper;
using GbAviationTicketApi.Models.Dtos;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GbAviationTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorsController : BaseController<OperatorsController>
    {
        public OperatorsController(IRepositoryWrapper repository, IMapper mapper) : base(repository, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOperators()
        {
            var terminal = GetCurrentUserTerminal();
            var operators = (await _repository.Users
                .FindByConditionAsync(u => u.TerminalId == terminal && u.Role.Name == OP_ROLE )).ToList();

            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = _mapper.Map<List<UserDto>>(operators);
            return Ok(apiResponse);
        }

        [HttpGet("username", Name = nameof(GetOperatorByUserName))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOperatorByUserName(string username)
        {
            var terminal = GetCurrentUserTerminal();
            var _operator = (await _repository.Users
                .FindByConditionAsync(u => u.TerminalId == terminal && u.Role.Name == OP_ROLE &&
                u.UserName == username.ToLower())).FirstOrDefault();

            if (_operator == null)
                return FailResponse(HttpStatusCode.NotFound, $"Operator with username {username} not found");

            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = _operator;
            return Ok(apiResponse);
        }
    }
}

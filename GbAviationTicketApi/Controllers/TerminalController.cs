using AutoMapper;
using GbAviationTicketApi.Models.Dtos;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GbAviationTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TerminalController : BaseController<TerminalController>
    {
        
        public TerminalController(IRepositoryWrapper repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTerminalsAsync()
        {
            var terminals = _mapper.Map<List<TerminalDto>>((await _repository.Terminals.FindAllAsync()).ToList());
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = terminals;

            return Ok(apiResponse);
            
        }
    }
}

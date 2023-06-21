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
    public class CustomerController : BaseController<CustomerController>
    { 
        public CustomerController(IRepositoryWrapper repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        [HttpGet(Name = nameof(GetCustomersAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = _mapper.Map<List<CustomerDto>>(await _repository.Customers.GetAllCustomers());
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = result;

            return Ok(apiResponse);
        }

    }
}

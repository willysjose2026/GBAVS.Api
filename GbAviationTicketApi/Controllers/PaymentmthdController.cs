using AutoMapper;
using GbAviationTicketApi.Models.Dtos;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GbAviationTicketApi.Controllers
{
    [Route("api/PaymentMethod")]
    [ApiController]
    [Authorize]
    public class PaymentmthdController : BaseController<PaymentmthdController>
    {
        public PaymentmthdController(IRepositoryWrapper repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaymentmethodsAsync()
        {
            var results = _mapper.Map<List<PaymentmthdDto>>((await _repository.Paymentmthds.FindAllAsync()).ToList());
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = results;
            return Ok(results);
        }
    }
}

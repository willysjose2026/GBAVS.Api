using AutoMapper;
using GbAviationTicketApi.Extentions;
using GbAviationTicketApi.Models;
using GbAviationTicketApi.Models.Dtos;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GbAviationTicketApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = $"{OP_ROLE},{ADMIN_ROLE}")]

    public class TicketsController : BaseController<TicketsController>
    {
        public TicketsController(IRepositoryWrapper repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        [HttpGet(Name = nameof(GetTicketsAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetTicketsAsync()
        {
            var ticketList = (await _repository.Tickets.FindAllAsync()).ToList();
            var result = _mapper.Map<List<TicketDto>>(ticketList);
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = result;
            return Ok(apiResponse);
        }


        [HttpGet("{ticket_no}", Name = nameof(GetTicketsByNoAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTicketsByNoAsync(string ticket_no)
        {
            if(ticket_no.Length < 8)
                return FailResponse(null, "number of digits in ticket number is invalid");
            
            var ticket = await (await _repository.Tickets.FindByConditionAsync(t => t.TicketNo == ticket_no))
                .FirstOrDefaultAsync();
            
            if (ticket == null)
                return FailResponse(HttpStatusCode.NotFound, $"Ticket with No. {ticket_no} was not found");
            
            var result = _mapper.Map<TicketDto>(ticket);
            
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = result;
            return Ok(apiResponse);
        }

        //TODO: create ticket no. sequence table and create code to autogenerate ticket no with sequence
        private string GenerateTickets(int idCategory, int lastIndex)
        {
            return idCategory + lastIndex.ToString().PadLeft(6,'0');
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTicket(TicketCreateDto createDto)
        {
            if (createDto == null)
                return FailResponse(null, "Null ticket or null ticket field");

            createDto.Normalize();

            var isValidNo = (await _repository.Tickets.FindByConditionAsync(t => t.TicketNo == createDto.TicketNo))
                .FirstOrDefault() == null;

            if (!isValidNo)
                return FailResponse(null, "ticket no. entered already exists");

            if (createDto.IsValid(out List<string> validableResult))
            {
                var ticketToValidate = _mapper.Map<TicketDto>(createDto);
                var dtoInvalid = await CheckIsValidTicketAsync(ticketToValidate);

                if (dtoInvalid != null)
                    return dtoInvalid;

                var ticket = _mapper.Map<Ticket>(createDto);

                if (ticket.InitTime > ticket.EndTime)
                    return FailResponse(null, "Init time cannot be bigger than end time");
                //TODO: create validation for when time starts before midnight and ends after it

                await _repository.Tickets.CreateAsync(ticket);
                apiResponse.StatusCode = HttpStatusCode.OK;
                apiResponse.Result = createDto;
                return CreatedAtRoute(nameof(GetTicketsByNoAsync), new { ticket_no = createDto.TicketNo }, apiResponse);
            }

            return FailResponse(null, validableResult.ToArray());

        }

        [HttpPut("{ticket_no}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTicket(string ticket_no, TicketUpdateDto updateDto)
        {
            if (updateDto == null || string.IsNullOrEmpty(ticket_no) || ticket_no != updateDto.TicketNo)
                return FailResponse(null, "Null ticket. See if ticket No match with the body Ticket No");

            updateDto.Normalize();

            if(updateDto.IsValid(out List<string> errorMessages))
            {
                var dtoValid = await CheckIsValidTicketAsync(_mapper.Map<TicketDto>(updateDto));
                if (dtoValid != null)
                    return dtoValid;

                var ticket = _mapper.Map<Ticket>(updateDto);
                var result = await _repository.Tickets.UpdateAsync(ticket);

                if (result != null)
                    apiResponse.Result = updateDto;

                apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(apiResponse);
            }

            return FailResponse(null, errorMessages.ToArray());
        }

        [HttpDelete("{ticket_no}")]
        public async Task<IActionResult> DeleteTicket(string ticket_no)
        {
            if (string.IsNullOrEmpty(ticket_no))
                return FailResponse(null, "Null Ticket No");

            var ticket = (await _repository.Tickets.FindByConditionAsync(t => t.TicketNo == ticket_no))
                .FirstOrDefault();

            if (ticket == null)
                return FailResponse(HttpStatusCode.NotFound, "ticket not found");

            await _repository.Tickets.DeleteAsync(ticket);
            apiResponse.StatusCode = HttpStatusCode.OK;
            return Ok(apiResponse);
        }

        private async Task<IActionResult?> CheckIsValidTicketAsync(TicketDto dto)
        {
            var customer = (await _repository.Customers.FindByConditionAsync(c => c.Id == dto.CustomerId)).FirstOrDefault();
            if (customer == null)
                return FailResponse(null, "customer with entered ID was not found");

            var product = (await _repository.Products.FindByConditionAsync(c => c.Id == dto.ProductId)).FirstOrDefault();
            if (product == null)
                return FailResponse(null, "product with entered ID was not found");

            var terminal = (await _repository.Terminals.FindByConditionAsync(c => c.Id == dto.TerminalId)).FirstOrDefault();
            if (terminal == null)
                return FailResponse(null, "terminal with entered ID was not found");

            if (dto.TicketNo[..2] != terminal.Id.ToString())
                return FailResponse(null, "terminal ID must match 2 first digits of ticket no");

            var paymentMethod = (await _repository.Paymentmthds.FindByConditionAsync(c => c.Id == dto.PaymentMthdId)).FirstOrDefault();
            if (paymentMethod == null)
                return FailResponse(null, "payment method with entered ID was not found");

            var t_operator = (await _repository.Users.FindByConditionAsync(u => u.UserName == dto.OpUserName)).FirstOrDefault();
            if (t_operator == null)
                return FailResponse(null, "operator with entered username does not exists");

            return null;
        }
    }
}

using AutoMapper;
using GbAviationTicketApi.Extentions;
using GbAviationTicketApi.Models;
using GbAviationTicketApi.Models.Dtos;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GbAviationTicketApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = $"{ARP_ROLE},{ADMIN_ROLE}")]
    [ApiController]
    public class TicketReportController : BaseController<TicketReportController>
    {
        public TicketReportController(IRepositoryWrapper repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSummaries()
        {
            var user = HttpContext.User.Identity?.Name;
            var summaries = (await _repository.TicketReports
                    .FindByConditionAsync(sm => sm.AgentUserName == (user ?? ""))).ToList();
            
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = _mapper.Map<List<ReportSummaryDto>>(summaries);
            return Ok(apiResponse);
        }

        [HttpGet("{id:int}", Name = nameof(GetSummaryById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSummaryById(int id)
        {
            var user = HttpContext.User.Identity?.Name;
            var summary = (await _repository.TicketReports
                .FindByConditionAsync(s => s.Id == id && s.AgentUserName == (user ?? ""))).FirstOrDefault();
            if (summary != null)
            {
                apiResponse.StatusCode = HttpStatusCode.OK;
                apiResponse.Result = _mapper.Map<ReportSummaryDto>(summary);
                return Ok(apiResponse);
            }

            return FailResponse(HttpStatusCode.NotFound, "Report was not found");
        }

        [HttpPost(nameof(CreateReportSummary))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateReportSummary(ReportSummaryCreateDto createDto)
        {
            if (createDto == null)
                return FailResponse(null, "null reference, check if your fields are ok");

            createDto.Normalize();
            if (createDto.IsValid(out List<string> errorMessages))
            {
                if (createDto.OperatorUserName == string.Empty)
                    createDto.OperatorUserName = null;

                var dtoInvalid = await CheckIsValidSummaryAsync(createDto.StartDate, createDto.EndDate, 
                    createDto.AgentUserName, createDto.OperatorUserName, createDto.TerminalId);

                if (dtoInvalid != null)
                    return dtoInvalid;

                var duplicate = (await _repository.TicketReports.FindByConditionAsync(rs => 
                    rs.OperatorUserName == createDto.OperatorUserName && rs.AgentUserName == createDto.AgentUserName 
                    && rs.StartDate == createDto.StartDate && rs.EndDate == createDto.EndDate && rs.TerminalId == createDto.TerminalId))
                    .FirstOrDefault();

                if (duplicate != null)
                    return FailResponse(null, "report already exists");

                var reportSummary = _mapper.Map<ReportSummary>(createDto);
                var result = await _repository.TicketReports.CreateReportSummary(reportSummary);
                apiResponse.StatusCode = HttpStatusCode.OK;
                apiResponse.Result = createDto;
                return CreatedAtRoute(nameof(GetSummaryById),
                    new { id = result.Id }, apiResponse);

            }

            return FailResponse(null, errorMessages.ToArray());
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateReportSummary(int id, ReportSummaryUpdateDto updateDto)
        {
            if (id == 0 || updateDto == null || id != updateDto.Id)
                return FailResponse(null, "invalid Id or Null Reference on Report Body");

            updateDto.Normalize();
            
            if(updateDto.IsValid(out List<string> errorMessages))
            {
                var summary = (await _repository.TicketReports
                    .FindByConditionAsync(s => s.Id == updateDto.Id)).FirstOrDefault();

                if (summary == null)
                    return FailResponse(HttpStatusCode.NotFound, "Report Summary Not Found");

                if (updateDto.OperatorUserName == string.Empty)
                    updateDto.OperatorUserName = null;

                var dtoInvalid = await CheckIsValidSummaryAsync(updateDto.StartDate, updateDto.EndDate, updateDto.AgentUserName,
                    updateDto.OperatorUserName, updateDto.TerminalId);

                if (dtoInvalid != null)
                    return dtoInvalid;

                var duplicate = (await _repository.TicketReports.FindByConditionAsync(rs => rs.OperatorUserName == updateDto.OperatorUserName
                    && rs.AgentUserName == updateDto.AgentUserName && rs.StartDate == updateDto.StartDate 
                    && rs.EndDate == updateDto.EndDate && rs.TerminalId == updateDto.TerminalId))
                    .FirstOrDefault();

                if (duplicate != null)
                    return FailResponse(null, "report already exists");

                _mapper.Map(updateDto, summary);
                var result = await _repository.TicketReports.SimpleUpdateAsync(summary);
                
                if(result != null)
                {
                    apiResponse.StatusCode = HttpStatusCode.OK;
                    apiResponse.Result = updateDto;
                    return Ok(apiResponse);
                }
            }

            return FailResponse(null, errorMessages.ToArray());
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> DeleteReportSummary(int id)
        {
            if (id == 0)
                return FailResponse(null, "invalid Id");

            var summary = (await _repository.TicketReports
                .FindByConditionAsync(s => s.Id == id)).FirstOrDefault();

            if (summary == null)
                return FailResponse(HttpStatusCode.NotFound, "Report Summary Not Found");

            await _repository.TicketReports.DeleteAsync(summary);
            apiResponse.StatusCode = HttpStatusCode.OK;
            return Ok(apiResponse);

        }

        [HttpPost(nameof(GenerateTicketReport))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GenerateTicketReport(ReportRequestDto request)
        {
            if (request == null)
                return FailResponse(null, "null reference, check if your fields are ok");

            request.Normalize();

            if (request.IsValid(out List<string> errorMesseges))
            {
                if (request.OperatorUserName == string.Empty)
                    request.OperatorUserName = null;

                var dtoInvalid = await CheckIsValidSummaryAsync(request.StartDate, request.EndDate, 
                    request.AgentUserName, request.OperatorUserName, request.TerminalId);

                if (dtoInvalid != null)
                    return dtoInvalid;

                var report_request = _mapper.Map<ReportSummary>(request);
                var report = await _repository.TicketReports.GenerateTicketReport(report_request);

                if (report != null)
                    apiResponse.Result = report;

                apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(apiResponse);
            }

            return FailResponse(null, errorMesseges.ToArray());
        }

        private async Task<bool> IsUserValidAsync(string _operator)
        {
            return await _repository.Users
                    .IsUniqueUserAsync(_operator) == false;
        }

        private async Task<bool> IsTerminalValidAsync(int terminal)
        {
            return (await _repository.Terminals
                    .FindByConditionAsync(t => t.Id == terminal)).FirstOrDefault() != null;
        }

        private async Task<IActionResult?> CheckIsValidSummaryAsync(DateTime startDate, DateTime endDate, 
            string agentUserName, string? operatorUserName, int terminalId)
        {
            if (startDate.Date >= endDate.Date)
                return FailResponse(null, $"Start Date can't be equal or bigger than End Date");

            if (!await IsUserValidAsync(agentUserName))
                return FailResponse(null, $"invalid agent username");

            if (operatorUserName != null &&
                !await IsUserValidAsync(operatorUserName))
                return FailResponse(null, $"invalid operator username");

            if (!await IsTerminalValidAsync(terminalId))
                return FailResponse(null, $"invalid terminal");

            return null;
        }
    }
}

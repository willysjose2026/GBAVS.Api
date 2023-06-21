using AutoMapper;
using GbAviationTicketApi.Models;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace GbAviationTicketApi.Controllers
{
    [Authorize(Policy = "DeviceCheckPolicy")]
    public abstract class BaseController<T> : ControllerBase where T : class
    {
        protected readonly IRepositoryWrapper _repository;
        protected readonly IMapper _mapper;
        protected ApiResponse apiResponse;

        protected const string ARP_ROLE = "ARP_AGENT";
        protected const string ADMIN_ROLE = "ADMIN";
        protected const string OP_ROLE = "OPERATOR";

        protected BaseController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            apiResponse = new();

            
        }

        protected bool IsUnauthorized() => 
            Response.StatusCode == StatusCodes.Status401Unauthorized;

        protected IActionResult FailResponse(HttpStatusCode? code, params string[] message)
        {
            code ??= HttpStatusCode.BadRequest;
            apiResponse.StatusCode = (HttpStatusCode)code;
            apiResponse.IsSuccess = false;
            apiResponse.ErrorMesseges.AddRange(message);
            return StatusCode((int)code, apiResponse);
        }

        protected int GetCurrentUserTerminal()
           => int.Parse(HttpContext.User.Claims.Where(c => c.Type == "Terminal").FirstOrDefault()?.Value ?? "0");

        protected string GetCurrentUserName()
           => HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault()?.Value ?? "";

    }
}

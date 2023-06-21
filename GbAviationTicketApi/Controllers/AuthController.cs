using AutoMapper;
using EmailService;
using EmailService.Contracts;
using GbAviationTicketApi.Extentions;
using GbAviationTicketApi.Models.Dtos;
using GbAviationTicketApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GbAviationTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController<AuthController>
    {
        public AuthController(IRepositoryWrapper repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username)
                || string.IsNullOrEmpty(loginRequest.Password))
            {
                return FailResponse(null, "null object reference");
            }

            loginRequest.Normalize();

            if (await _repository.Users.IsUniqueUserAsync(loginRequest.Username))
                return FailResponse(null, "invalid username");

            if (loginRequest.IsValid(out List<string> errorResults))
            {
                var response = await _repository.Users.Login(loginRequest);
                if (response == null)
                    return FailResponse(HttpStatusCode.InternalServerError, "Error trying to log in");

                if (response.User == null || string.IsNullOrEmpty(response.Token))
                    return FailResponse(null, "invalid password");

                apiResponse.StatusCode = HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                apiResponse.Result = response;
                return Ok(apiResponse);
            }

            return FailResponse(null, errorResults.ToArray());
        }

        //[Authorize(Roles = $"{ADMIN_ROLE}")]
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(RegistrationDto registrationDto)
        {
            if (registrationDto == null)
            {
                return FailResponse(null, "null object reference");
            }

            var isValidTerminal = (await _repository.Terminals.FindByConditionAsync(
                t => t.Id == registrationDto.TerminalId)).FirstOrDefault() != null;

            if (!isValidTerminal)
            {
                return FailResponse(null, $"terminal with id {registrationDto.TerminalId} was not found");
            }

            var isUniqueUser = await _repository.Users.IsUniqueUserAsync(registrationDto.Username);
            if (!isUniqueUser)
            {
                return FailResponse(null, $"username {registrationDto.Username} already in use");
            }

            registrationDto.Normalize();
            if (registrationDto.IsValid(out List<string> ErrorResult))
            {
                var user = await _repository.Users.Register(registrationDto);
                if (user != null)
                {
                    apiResponse.StatusCode = HttpStatusCode.OK;
                    apiResponse.IsSuccess = true;
                    apiResponse.Result = user;
                    return Ok(apiResponse);
                }

                return FailResponse(HttpStatusCode.InternalServerError, "Error While Registring User");

            }
            return FailResponse(null, ErrorResult.ToArray());

        }
    }
}

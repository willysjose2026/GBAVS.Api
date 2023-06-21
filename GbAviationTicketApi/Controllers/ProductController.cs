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
    public class ProductController : BaseController<ProductController>
    {
        
        public ProductController(IRepositoryWrapper repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        [HttpGet(Name = nameof(GetProductsAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = _mapper.Map<List<ProductDto>>((await _repository.Products.FindAllAsync()).ToList());
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = result;

            return Ok(apiResponse);
        }
    }
}

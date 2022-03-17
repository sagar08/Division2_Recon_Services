using Division2ReconService.Data;
using Division2ReconService.Infrastructure;
using Division2ReconService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Division2ReconService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly Division2ReconDbContext _dbContext;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(Division2ReconDbContext dbContext,
                                                    ILogger<CustomerController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetCustomers")]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCustomers()
        {
            try
            {
                var customers = _dbContext.Customers.ToList();
                if (customers == null) return NotFound();

                var response = new ResponseDto<List<Customer>>
                {
                    Success = true,
                    Message = Constants.Messages.Data_Found,
                    Data = customers
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetHelloWorld")]
        public async Task<ActionResult> GetHelloWorld()
        {
            return Ok("Hello World");
        }
    }
}

using AutoMapper;
using Division2ReconService.Data;
using Division2ReconService.Infrastructure;
using Division2ReconService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Division2ReconService.Controllers
{
    /// <summary>
    ///  Customer Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly Division2ReconDbContext _dbContext;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public CustomerController(Division2ReconDbContext dbContext,
                                                    ILoggerManager logger,
                                                    IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all active customers
        /// </summary>
        /// <returns>List of customers</returns>        
        [HttpGet]
        [Route("GetCustomers")]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCustomers()
        {
            _logger.LogInfo("Fetching data");
            var dbCustomers = _dbContext.Customers.ToList();
            if (dbCustomers == null) return NotFound();

            var customers = _mapper.Map<List<CustomerResponseDto>>(dbCustomers);

            var response = new ResponseDto<List<CustomerResponseDto>>
            {
                Success = true,
                Message = Constants.Messages.Data_Found,
                Data = customers
            };
            return Ok(response);
        }      
    }
}

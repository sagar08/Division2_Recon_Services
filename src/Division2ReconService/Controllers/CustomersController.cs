using AutoMapper;
using Division2ReconService.Data;
using Division2ReconService.Infrastructure;
using Division2ReconService.Models;
using Division2ReconService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Division2ReconService.Controllers
{
    /// <summary>
    ///  Customer Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ICustomerService _customerService;


        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="customerService"></param>
        public CustomersController(ILoggerManager logger,
                                                    ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        /// <summary>
        /// Get active customers
        /// </summary>
        /// <returns>List of customers</returns>        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetActiveCustomers()
        {
            _logger.LogInfo("Fetching active customers");
            var customers = await _customerService.GetActiveCustomersAsync();
            if (customers == null) return NotFound();

            var response = new ResponseDto<List<CustomerResponseDto>>
            {
                Success = true,
                Message = Constants.Messages.Data_Found,
                Data = customers
            };

            return Ok(response);
        }

        /// <summary>
        /// Get customers machines
        /// </summary>
        /// <returns>List of customers</returns>        
        [HttpGet]
        [Route("Machines")]
        [ProducesResponseType(typeof(IEnumerable<CustomerMachineResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCustomerMachines()
        {
            var machines = await _customerService.GetCustomerMachinesAsync();
            if (machines == null) return NotFound();

            var response = new ResponseDto<List<CustomerMachineResponseDto>>
            {
                Success = true,
                Message = Constants.Messages.Data_Found,
                Data = machines
            };

            return Ok(response);
        }
    }
}

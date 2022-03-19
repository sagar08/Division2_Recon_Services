using AutoMapper;
using Division2ReconService.Data;
using Division2ReconService.Infrastructure;
using Division2ReconService.Models;
using Microsoft.EntityFrameworkCore;

namespace Division2ReconService.Services;

/// <summary>
/// Customer Service
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly Division2ReconDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;

    /// <summary>
    /// 
    /// </summary>
    public CustomerService(Division2ReconDbContext dbContext,
                                                    ILoggerManager logger,
                                                    IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Get active customers
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<List<CustomerResponseDto>?> GetActiveCustomersAsync()
    {
        var dbCustomers = await _dbContext.Customers.Where(c => c.IsActive).ToListAsync();
        if (dbCustomers == null) return null;

        var customers = _mapper.Map<List<CustomerResponseDto>>(dbCustomers);
        return customers;
    }

    /// <summary>
    /// Get Customer machines
    /// </summary>
    /// <returns></returns>
    public async Task<List<CustomerMachineResponseDto>?> GetCustomerMachinesAsync()
    {
        var machines = await _dbContext.Machines
                                                .Join(_dbContext.Customers,
                                                m => m.CustomerId,
                                                c => c.Id,
                                                (m, c) => new CustomerMachineResponseDto
                                                {
                                                   CustomerId = c.CustomerId,
                                                   CustomerName = c.CustomerName,
                                                   MachineId = m.MachineId,
                                                   MachineNumber = m.MachineNumber,
                                                   MachineTypeSerial = m.MachineTypeSerial,
                                                   Process = m.Process,
                                                   ProcessStartTime = m.ProcessStartTime,
                                                   ProcessEndTime = m.ProcessEndTime,
                                                   SensorData = m.SensorData,
                                                   Online = m.Online,
                                                   OnlineFrom = m.OnlineFrom,
                                                   OnlineTo = m.OnlineTo
                                                }
                                        ).ToListAsync();

        if (machines == null) return null;        
        return machines;
    }
}


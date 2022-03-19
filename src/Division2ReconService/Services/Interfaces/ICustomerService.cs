using Division2ReconService.Models;

namespace Division2ReconService.Services;

/// <summary>
///  Customer Service Contract
/// </summary>
public interface ICustomerService
{
    /// <summary>
    /// Get active customers
    /// </summary>
    /// <returns></returns>
    Task<List<CustomerResponseDto>?> GetActiveCustomersAsync();

    /// <summary>
    /// Get Customer Machines
    /// </summary>
    /// <returns></returns>
    Task<List<CustomerMachineResponseDto>?> GetCustomerMachinesAsync();
}


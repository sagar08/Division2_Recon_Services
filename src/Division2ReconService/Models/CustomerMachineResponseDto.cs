namespace Division2ReconService.Models;

/// <summary>
/// Customer Machines Dto
/// </summary>
public class CustomerMachineResponseDto
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; }

    public int MachineId { get; set; }

    public string MachineNumber { get; set; }

    public string MachineTypeSerial { get; set; }

    public string Process { get; set; }

    public DateTime ProcessStartTime { get; set; }

    public DateTime ProcessEndTime { get; set; }

    public string SensorData { get; set; }

    public bool Online { get; set; }

    public DateTime? OnlineFrom { get; set; }

    public DateTime? OnlineTo { get; set; }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Division2ReconService.Data;

/// <summary>
/// Machine context
/// </summary>
public class Machine
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MachineId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(50)]
    public string MachineNumber { get; set; }

    [StringLength(250)]
    public string MachineTypeSerial { get; set; }

    [Required]
    [StringLength(50)]
    public string Process { get; set; }

    public DateTime ProcessStartTime { get; set; }

    public DateTime ProcessEndTime { get; set; }

    [StringLength(250)]
    public string SensorData { get; set; }

    public bool Online { get; set; }

    public DateTime? OnlineFrom { get; set; }

    public DateTime? OnlineTo { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Customer Customer { get; set; }
}


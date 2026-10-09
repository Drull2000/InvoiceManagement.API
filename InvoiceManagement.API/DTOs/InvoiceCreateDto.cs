using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.API.DTOs;

public class InvoiceCreateDto
{
    [Required]
    public string ClientName { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal Total { get; set; }
}

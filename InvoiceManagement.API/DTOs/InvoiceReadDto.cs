namespace InvoiceManagement.API.DTOs;

public class InvoiceReadDto
{
    public string Number { get; set; } = string.Empty;

    public string ClientName { get; set; } = string.Empty;

    public decimal Total { get; set; }
}

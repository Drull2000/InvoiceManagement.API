using AutoMapper;
using InvoiceManagement.API.DTOs;
using InvoiceManagement.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private static readonly List<Invoice> Invoices =
    [
        new Invoice
        {
            Id = 1,
            Number = "INV-001",
            ClientName = "ООО Ромашка",
            Total = 12500.00m,
            CreatedAt = new DateTime(2026, 1, 10)
        },
        new Invoice
        {
            Id = 2,
            Number = "INV-002",
            ClientName = "ООО Вектор",
            Total = 8700.50m,
            CreatedAt = new DateTime(2026, 1, 15)
        },
        new Invoice
        {
            Id = 3,
            Number = "INV-003",
            ClientName = "ИП Соколова",
            Total = 3200.75m,
            CreatedAt = new DateTime(2026, 1, 20)
        }
    ];

    private readonly IMapper _mapper;

    public InvoicesController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<InvoiceReadDto>> GetAll()
    {
        return Ok(_mapper.Map<IEnumerable<InvoiceReadDto>>(Invoices));
    }

    [HttpGet("{id:int}")]
    public ActionResult<InvoiceReadDto> GetById(int id)
    {
        var invoice = Invoices.FirstOrDefault(invoice => invoice.Id == id);

        if (invoice is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<InvoiceReadDto>(invoice));
    }

    [HttpPost]
    public ActionResult<InvoiceReadDto> Create(InvoiceCreateDto createDto)
    {
        if (string.IsNullOrWhiteSpace(createDto.ClientName))
        {
            return BadRequest("ClientName is required.");
        }

        var invoice = _mapper.Map<Invoice>(createDto);
        invoice.Id = Invoices.Count == 0 ? 1 : Invoices.Max(existing => existing.Id) + 1;
        invoice.Number = $"INV-{invoice.Id:D3}";
        invoice.CreatedAt = DateTime.UtcNow;

        Invoices.Add(invoice);

        var result = _mapper.Map<InvoiceReadDto>(invoice);
        return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, result);
    }

    [HttpPut("{id:int}")]
    public ActionResult<InvoiceReadDto> Update(int id, InvoiceUpdateDto updateDto)
    {
        var invoice = Invoices.FirstOrDefault(invoice => invoice.Id == id);

        if (invoice is null)
        {
            return NotFound();
        }

        _mapper.Map(updateDto, invoice);

        return Ok(_mapper.Map<InvoiceReadDto>(invoice));
    }
}

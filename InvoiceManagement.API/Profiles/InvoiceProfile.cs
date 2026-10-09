using AutoMapper;
using InvoiceManagement.API.DTOs;
using InvoiceManagement.API.Models;

namespace InvoiceManagement.API.Profiles;

public class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<Invoice, InvoiceReadDto>().ReverseMap();
        CreateMap<Invoice, InvoiceCreateDto>().ReverseMap();
        CreateMap<Invoice, InvoiceUpdateDto>().ReverseMap();
    }
}

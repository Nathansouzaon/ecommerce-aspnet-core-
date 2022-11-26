using App.Pedidos.API.Application.DTO;
using App.Pedidos.Domain.Vouchers;
using AutoMapper;

namespace App.Pedidos.API.Automapper
{
    public class VoucherProfile : Profile
    {
        public VoucherProfile()
        {
            CreateMap<VoucherAdicionarDTO, Voucher>();
            CreateMap<Voucher, VoucherDTO>().ReverseMap();
        }
    }
}

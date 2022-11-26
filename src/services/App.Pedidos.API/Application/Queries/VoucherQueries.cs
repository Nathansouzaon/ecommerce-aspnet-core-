using App.Pedidos.API.Application.DTO;
using App.Pedidos.API.Contracts;
using App.Pedidos.Domain.Contracts;
using AutoMapper;

namespace App.Pedidos.API.Application.Queries
{
    public class VoucherQueries : IVoucherQueries
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IMapper _mappper;

        public VoucherQueries(IVoucherRepository voucherRepository, IMapper mapper)
        {
            _voucherRepository = voucherRepository;
            _mappper = mapper;
        }

        public async Task<IEnumerable<VoucherDTO>> ObterTodos()
        {
            var vouchers = await _voucherRepository.ObterTodos();
            List<VoucherDTO> vouchersList = new();

            if (vouchers is null) return null;

            foreach (var voucher in vouchers)
            {
                vouchersList.Add(new VoucherDTO
                {
                    Id = voucher.Id,
                    Ativo = voucher.Ativo,
                    Quantidade = voucher.Quantidade,
                    Codigo = voucher.Codigo,
                    TipoDesconto = (int)voucher.TipoDesconto,
                    Percentual = voucher.Percentual,
                    ValorDesconto = voucher.ValorDesconto,
                });
            }

            return vouchersList;
        }

        public async Task<VoucherDTO> ObterVoucherPorCodigo(string codigo)
        {
            var voucher = await _voucherRepository.ObterVoucherPorCodigo(codigo);

            if (voucher is null) return null;

            if (voucher.EstaValidoParaUtilizacao() is false) return null;

            return new VoucherDTO
            {
                Id = voucher.Id,
                Ativo = voucher.Ativo,
                Codigo = voucher.Codigo,
                Quantidade = voucher.Quantidade,
                TipoDesconto = (int)voucher.TipoDesconto,
                Percentual = voucher.Percentual,
                ValorDesconto = voucher.ValorDesconto
            };
        }

        public async Task<VoucherDTO> ObterVoucherPorId(Guid id)
        {
            if (id == Guid.Empty) return null;

            var voucher = await _voucherRepository.ObterPorId(id);

            return _mappper.Map<VoucherDTO>(voucher);
        }
    }
}

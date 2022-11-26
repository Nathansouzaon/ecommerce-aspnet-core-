using App.Pedidos.API.Application.DTO;
using App.Pedidos.API.Contracts;
using App.Pedidos.Domain.Contracts;
using App.Pedidos.Domain.Vouchers;
using App.WebAPI.Core.Controllers;
using App.WebAPI.Core.Identidade;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Pedidos.API.Controllers
{
    [Authorize]
    [Route("vouchers")]
    public class VoucherController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IVoucherQueries _voucherQueries;
        private readonly IVoucherRepository _voucherRepository;

        public VoucherController(IVoucherQueries voucherQueries, IVoucherRepository voucherRepository, IMapper mapper)
        {
            _voucherQueries = voucherQueries;
            _voucherRepository = voucherRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ClaimsAuthorize("Administrador", "Voucher")]
        public async Task<IEnumerable<VoucherDTO>> ObterTodos()
        {
            var vouchers = await _voucherQueries.ObterTodos();
            return vouchers;
        }

        [HttpGet("obter/{id}")]
        [ClaimsAuthorize("Administrador", "Voucher")]
        public async Task<VoucherDTO> ObterPorId(Guid id)
        {
            var voucher = await _voucherQueries.ObterVoucherPorId(id);
            return voucher;
        }

        [HttpGet("{codigo}")]
        [ProducesResponseType(typeof(VoucherDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ObterPorCodigo(string codigo)
        {
            if (string.IsNullOrEmpty(codigo)) return NotFound();

            var voucher = await _voucherQueries.ObterVoucherPorCodigo(codigo);

            return voucher is null ? NotFound() : CustomResponse(voucher);
        }

        [HttpPost]
        [ClaimsAuthorize("Administrador", "Voucher")]
        public async Task<IActionResult> Adicionar(VoucherAdicionarDTO voucherDTO)
        {
            if (ModelState.IsValid is false) return CustomResponse(ModelState);

            var voucher = _mapper.Map<Voucher>(voucherDTO);

            _voucherRepository.Adicionar(voucher);

            var result = await _voucherRepository.UnitOfWork.Commit();

            if (result is false) AdicionarErroProcessamento("Erro ao persistir dados na base de dados");

            return CustomResponse(voucherDTO);
        }

        [HttpDelete("{id}")]
        [ClaimsAuthorize("Administrador", "Voucher")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            if (id == Guid.Empty)
            {
                AdicionarErroProcessamento("Id inválido");
                return CustomResponse();
            }

            var voucher = await _voucherRepository.ObterPorId(id);

            if (voucher is null) return NotFound();

            voucher.MarcarComoExcluido();

            _voucherRepository.Atualizar(voucher);

            var result = await _voucherRepository.UnitOfWork.Commit();

            if (result is false) AdicionarErroProcessamento("Erro ao remover voucher");

            return CustomResponse();
        }

        [HttpPut("{id}")]
        [ClaimsAuthorize("Administrador", "Voucher")]
        public async Task<IActionResult> Atualizar(Guid id, VoucherDTO voucherDTO)
        {
            if (id == Guid.Empty) return NotFound();

            if (id != voucherDTO.Id) return NotFound();

            if (ModelState.IsValid is false) return CustomResponse(ModelState);

            _voucherRepository.Atualizar(_mapper.Map<Voucher>(voucherDTO));

            var result = await _voucherRepository.UnitOfWork.Commit();

            if (result is false) AdicionarErroProcessamento("Erro ao atualizar voucher");

            return CustomResponse();
        }
    }
}
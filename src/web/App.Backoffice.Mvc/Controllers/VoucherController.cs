using App.Backoffice.Mvc.Contracts;
using App.Backoffice.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Backoffice.Mvc.Controllers
{
    public class VoucherController : MainController
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _voucherService.ObterTodos());
        }

        [HttpGet]
        [Route("detalhes/{id}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            return View(await _voucherService.ObterPorId(id));
        }

        [HttpGet]
        [Route("adicionar")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [Route("adicionar")]
        public async Task<IActionResult> Adicionar(VoucherViewModel voucherViewModel)
        {
            if (ModelState.IsValid is false) return View(voucherViewModel);

            await _voucherService.Adicionar(voucherViewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("remover/{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            return View(await _voucherService.ObterPorId(id));
        }

        [HttpPost, ActionName("Remover")]
        [Route("remover/{id}")]
        public async Task<IActionResult> RemoverConfirmacao(Guid id)
        {
            var voucher = await _voucherService.ObterPorId(id);

            if (voucher is null) return NotFound();

            await _voucherService.Remover(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("editar/{id}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            return View(await _voucherService.ObterPorId(id));
        }

        [HttpPost]
        [Route("editar/{id}")]
        public async Task<IActionResult> Editar(Guid id, VoucherViewModel voucher)
        {
            if (id != voucher.Id) return NotFound();

            await _voucherService.Atualizar(id, voucher);

            return RedirectToAction(nameof(Index));
        }
    }
}
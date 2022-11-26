using App.WebApp.Mvc.Contracts;
using App.WebApp.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApp.Mvc.Controllers
{
    public class PedidoController : MainController
    {
        private readonly IClienteService _clienteService;
        private readonly IComprasBffService _comprasBffService;

        public PedidoController(IClienteService clienteService, IComprasBffService comprasBffService)
        {
            _clienteService = clienteService;
            _comprasBffService = comprasBffService;
        }

        [HttpGet]
        [Route("endereco-de-entrega")]
        public async Task<IActionResult> EnderecoEntrega()
        {
            var carrinho = await _comprasBffService.ObterCarrinho();

            if (carrinho.Itens.Count == 0) return RedirectToAction("Index", "Carrinho");

            var endereco = await _clienteService.ObterEndereco();
            var pedido = _comprasBffService.MapearParaPedido(carrinho, endereco);

            return View(pedido);
        }

        [HttpPost]
        [Route("finalizar-pedido")]
        public async Task<IActionResult> FinalizarPedido(PedidoTransacaoViewModel pedidoTransacao)
        {
            if (ModelState.IsValid is false) return View("Pagamento",
                _comprasBffService.MapearParaPedido(await _comprasBffService.ObterCarrinho(), null));

            var retorno = await _comprasBffService.FinalizarPedido(pedidoTransacao);

            if (ResponsePossuiErros(retorno))
            {
                var carrinho = await _comprasBffService.ObterCarrinho();

                if (carrinho.Itens.Count == 0) return RedirectToAction("Index", "Carrinho");

                var pedidoMap = _comprasBffService.MapearParaPedido(carrinho, null);

                return View("Pagamento", pedidoMap);
            }

            return RedirectToAction("PedidoConcluido");
        }

        [HttpGet]
        [Route("pagamento")]
        public async Task<IActionResult> Pagamento()
        {
            var carrinho = await _comprasBffService.ObterCarrinho();

            if (carrinho.Itens.Count == 0) return RedirectToAction("Index", "Carrinho");

            var pedido = _comprasBffService.MapearParaPedido(carrinho, null);

            return View(pedido);
        }

        [HttpGet]
        [Route("pedido-concluido")]
        public async Task<IActionResult> PedidoConcluido()
        {
            //return View("ConfirmacaoPedido", await _comprasBffService.ObterUltimoPedido()); 
            return View("ConfirmacaoPedido");
        }

        [HttpGet("meus-pedidos")]
        public async Task<IActionResult> MeusPedidos()
        {
            return View(await _comprasBffService.ObterListaPorClienteId());
        }
    }
}
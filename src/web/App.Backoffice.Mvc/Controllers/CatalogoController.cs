using App.Backoffice.Mvc.Contracts;
using App.Backoffice.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Backoffice.Mvc.Controllers
{
    [Route("produtos")]
    public class CatalogoController : MainController
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("vitrine")]
        public async Task<IActionResult> Index([FromQuery] int ps = 9, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var produtos = await _catalogoService.ObterTodos(ps, page, q);
            ViewBag.Pesquisa = q;
            produtos.ReferenceAction = "Index";
            return View(produtos);
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            return View("Detalhe", await _catalogoService.ObterPorId(id));
        }

        [HttpGet]
        [Route("adicionar")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [Route("adicionar")]
        public async Task<IActionResult> Adicionar(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid is false) return View(produtoViewModel);

            if (produtoViewModel.ImagemForumlario.Length > 0)
            {
                using var ms = new MemoryStream();
                produtoViewModel.ImagemForumlario.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                produtoViewModel.ImagemUpload = s;
                produtoViewModel.Imagem = produtoViewModel.ImagemForumlario.FileName;
            }

            await _catalogoService.Adicionar(produtoViewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("editar/{id}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            return View(await _catalogoService.ObterPorId(id));
        }

        [HttpPost]
        [Route("editar/{id}")]
        public async Task<IActionResult> Editar(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid is false) return View(produtoViewModel);

            if (produtoViewModel.ImagemForumlario?.Length > 0)
            {
                using var ms = new MemoryStream();
                produtoViewModel.ImagemForumlario.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                produtoViewModel.ImagemUpload = s;
                produtoViewModel.Imagem = produtoViewModel.ImagemForumlario.FileName;
            }

            await _catalogoService.Atualizar(id, produtoViewModel);

            return RedirectToAction(controllerName: "Home", actionName: "Index");
        }

        [HttpGet]
        [Route("remover/{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            return View(await _catalogoService.ObterPorId(id));
        }

        [HttpPost, ActionName("Remover")]
        [Route("remover/{id}")]
        public async Task<IActionResult> RemoverConfirmacao(Guid id)
        {
            if (id == Guid.Empty) return View();

            await _catalogoService.Remover(id);

            return RedirectToAction("Index");
        }
    }
}
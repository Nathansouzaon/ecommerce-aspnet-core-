using App.Catalogo.API.Contracts;
using App.Catalogo.API.Dtos;
using App.Catalogo.API.Models;
using App.WebAPI.Core.Controllers;
using App.WebAPI.Core.Identidade;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Catalogo.API.Controllers
{
    public class CatalogoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public CatalogoController(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet("catalogo/produtos")]
        public async Task<PagedResult<Produto>> Index([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            return await _produtoRepository.ObterTodos(ps, page, q);
        }

        [HttpGet("catalogo/produtos/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id)
        {
            return await _produtoRepository.ObterPorId(id);
        }

        [HttpGet("catalogo/produtos/lista/{ids}")]
        public async Task<IEnumerable<Produto>> ObterProdutosPorId(string ids)
        {
            return await _produtoRepository.ObterProdutosPorId(ids);
        }

        [Authorize]
        [ClaimsAuthorize("Administrador", "Produto")]
        [HttpPost("catalogo/produtos/adicionar")]
        public async Task<IActionResult> Adicionar(ProdutoAdicionarDto produtoDto)
        {
            if (ModelState.IsValid is false) return CustomResponse(ModelState);

            var imagemNome = Guid.NewGuid() + "_" + produtoDto.Imagem;

            if (UploadImagem(produtoDto.ImagemUpload, imagemNome) is false)
            {
                AdicionarErroProcessamento("Não foi possivel fazer upload da imagem");
                return CustomResponse();
            }

            produtoDto.Imagem = imagemNome;
            var produto = _mapper.Map<Produto>(produtoDto);

            _produtoRepository.Adicionar(produto);
            var result = await _produtoRepository.UnitOfWork.Commit();

            if (result is false) AdicionarErroProcessamento("Erro ao persistir produto");

            return CustomResponse(produtoDto);
        }

        private bool UploadImagem(string arquivo, string imagemNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                AdicionarErroProcessamento("Forneça uma imagem para este produto!");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(arquivo);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imagemNome);

            if (System.IO.File.Exists(filePath))
            {
                AdicionarErroProcessamento("Já existe um arquivo com este nome!");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }

        [Authorize]
        [ClaimsAuthorize("Administrador", "Produto")]
        [HttpPut("catalogo/produtos/atualizar/{id}")]
        public async Task<IActionResult> Atualizar(Guid id, ProdutoAtualizarDto produtoDto)
        {
            if (ModelState.IsValid is false) return CustomResponse(ModelState);

            var produtoAtual = await _produtoRepository.ObterPorId(id);

            if (produtoAtual is null)
            {
                AdicionarErroProcessamento("O produto informado não existe");
                return CustomResponse();
            }

            //caso a imagem tenha sido alterada
            if (produtoDto.ImagemUpload is not null)
            {
                var imagemNome = Guid.NewGuid() + "_" + produtoDto.Imagem;

                if (UploadImagem(produtoDto.ImagemUpload, imagemNome) is false)
                {
                    AdicionarErroProcessamento("Não foi possivel fazer upload da imagem");
                    return CustomResponse();
                }

                produtoAtual.Imagem = imagemNome;
            }

            produtoAtual.Nome = produtoDto.Nome;
            produtoAtual.Descricao = produtoDto.Descricao;
            produtoAtual.Ativo = produtoDto.Ativo;
            produtoAtual.Valor = produtoDto.Valor;
            produtoAtual.QuantidadeEstoque = produtoDto.QuantidadeEstoque;

            _produtoRepository.Atualizar(produtoAtual);

            var result = await _produtoRepository.UnitOfWork.Commit();
            if (result is false) AdicionarErroProcessamento("Erro ao persistir produto");

            return CustomResponse(produtoDto);
        }

        [Authorize]
        [ClaimsAuthorize("Administrador", "Produto")]
        [HttpDelete("catalogo/produtos/remover/{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            var produto = await _produtoRepository.ObterPorId(id);

            if (produto is null)
            {
                AdicionarErroProcessamento("O produto não existe");
                return CustomResponse();
            }

            produto.MarcarComoExcluido();

            _produtoRepository.Atualizar(produto);

            var result = await _produtoRepository.UnitOfWork.Commit();
            if (result is false) AdicionarErroProcessamento("Erro ao persistir produto");

            return CustomResponse();
        }
    }
}
using App.Catalogo.API.Dtos;
using App.Catalogo.API.Models;
using AutoMapper;

namespace App.Catalogo.API.Automapper
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<ProdutoAdicionarDto, Produto>();
        }
    }
}

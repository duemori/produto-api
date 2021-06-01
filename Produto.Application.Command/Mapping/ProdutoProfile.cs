using AutoMapper;
using Produto.Application.Command.Commands;
using Produto.Domain.Entities;

namespace Produto.Application.Command.Mapping
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<CriarProdutoCommand, ProdutoEntity>();
            CreateMap<AtualizarProdutoCommand, ProdutoEntity>();
        }
    }
}

using AutoMapper;
using ControleEstoqueApi.Dtos;
using ControleEstoqueApi.Entities;

namespace ControleEstoqueApi.Profiles
{
  public class CadastroProfile : Profile
  {
    public CadastroProfile()
    {
      CreateMap<Produto, ProdutoDTO>().ReverseMap();
    }
  }
}

using ControleEstoqueApi.Entities;

namespace ControleEstoqueApi.IRepositories
{
  public interface IProdutoRepository 
  {
    Task<bool> Existe(Produto entity);
  }
}

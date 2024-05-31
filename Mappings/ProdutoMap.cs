using ControleEstoqueApi.Entities;
using FluentNHibernate.Mapping;

namespace ControleEstoqueApi.Mappings
{
  public class ProdutoMap : ClassMap<Produto>
  {
    public ProdutoMap() 
    {
      Table("Produtos");

      Id(x => x.Id);
      Map(x => x.Nome).Not.Nullable();
      Map(x => x.Descricao);
      Map(x => x.QuantidadeEstoque).Not.Nullable();
    }
  }
}

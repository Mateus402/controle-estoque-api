using ControleEstoqueApi.Entities;
using FluentNHibernate.Mapping;

namespace ControleEstoqueApi.Mappings
{
  public class MovimentacaoMap : ClassMap<Movimentacao>
  {
    public MovimentacaoMap()
    {
      Table("Movimentacoes");
      Id(x => x.Id);
      References(x => x.Produto).Column("ProdutoId").Not.Nullable();
      Map(x => x.Quantidade).Not.Nullable();
      Map(x => x.Data).Not.Nullable();
      Map(x => x.Tipo).Not.Nullable();
    }
  }
}

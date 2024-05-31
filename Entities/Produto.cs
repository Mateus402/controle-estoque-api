namespace ControleEstoqueApi.Entities
{
  public class Produto
  {
    public virtual long Id {get; set;}
    public virtual string Nome { get; set; } = null!;
    public virtual string Descricao { get; set; } = null!;
    public virtual int QuantidadeEstoque { get; set; }
  }
}

namespace ControleEstoqueApi.Entities
{
  public class Movimentacao
  {
    public virtual long Id { get; set; }
    public virtual long ProdutoId { get; set; }
    public virtual int Quantidade { get; set; }
    public virtual string Tipo { get; set; } = null!;
    public virtual DateTime Data { get; set; }
    public virtual Produto Produto { get; set; } = null!;
  }
}

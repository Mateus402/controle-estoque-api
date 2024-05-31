namespace ControleEstoqueApi.Dtos
{
  public class ProdutoDTO
  {
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public decimal Preco { get; set; }
    public int QuantidadeEstoque { get; set; }
  }
}

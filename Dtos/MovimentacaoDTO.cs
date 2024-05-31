namespace ControleEstoqueApi.Dtos
{
  public class MovimentacaoDTO
  {
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public DateTime Data { get; set; }
    public string Tipo { get; set; } = null!;
  }
}

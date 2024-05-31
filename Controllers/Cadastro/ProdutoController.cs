using ControleEstoqueApi.Dtos;
using ControleEstoqueApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleEstoqueApi.Controllers.Cadastro;

/// <summary>
/// Informações do Cadastro do Produto
/// </summary>
[Route("api/cadastro/produto")]
[ApiController]
public class ProdutoController : ControllerBase
{
  private readonly ProdutoService _produtoService;

  public ProdutoController(ProdutoService produtoService)
  {
    _produtoService = produtoService;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="skip"></param>
  /// <param name="take"></param>
  /// <returns></returns>
  [HttpGet]
  public async Task<ActionResult<List<ProdutoDTO>>> GetProdutos(
    [FromQuery] int skip,
    [FromQuery] int take
  )
  {
    return await _produtoService.GetProdutosAsync(skip, take);
  }

  /// <summary>
  /// Retorna um Produto pelo Id
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("{id}")]
  public async Task<ActionResult<ProdutoDTO>> GetProduto(
    [FromRoute] 
    long id
  )
  {    
    var produto = await _produtoService.GetProdutoByIdAsync(id);

    if (produto == null)
      return NotFound("Produto Não Encontrado!");

    return produto;
  }

  /// <summary>
  /// Criar um novo produto
  /// </summary>
  [HttpPost]
  [ProducesResponseType(200)]
  public async Task<ActionResult<ProdutoDTO>> AddProduto(ProdutoDTO produtoDto)
  {
    var newProduto = await _produtoService.AddProdutoAsync(produtoDto);
    return CreatedAtAction(nameof(GetProduto), new { id = newProduto.Id }, newProduto);
  }


}

using AutoMapper;
using ControleEstoqueApi.Dtos;
using ControleEstoqueApi.Entities;
using ControleEstoqueApi.IRepositories;
using NHibernate.Linq;

namespace ControleEstoqueApi.Services
{
  public class ProdutoService
  {
    private readonly NHibernate.ISession _session;
    private readonly IMapper _mapper;
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(NHibernate.ISession session, IMapper mapper, IProdutoRepository produtoRepository)
    {
      _session = session;
      _mapper = mapper;
      _produtoRepository = produtoRepository;
    }

    public async Task<List<ProdutoDTO>> GetProdutosAsync(int skip, int take)
    {
      var produtos = await _session.Query<Produto>().Skip(skip).Take(take).ToListAsync();

      return _mapper.Map<List<ProdutoDTO>>(produtos);
    }

    public async Task<ProdutoDTO> GetProdutoByIdAsync(long id)
    {
      var produto = await _session.GetAsync<Produto>(id);

      return _mapper.Map<ProdutoDTO>(produto);
    }


    public async Task<ProdutoDTO> AddProdutoAsync(ProdutoDTO produtoDto)
    {
      var produto = _mapper.Map<Produto>(produtoDto);

      if (await _produtoRepository.Existe(produto))
        throw new InvalidOperationException("Um produto com o mesmo nome já existe.");
      
      using (var transaction = _session.BeginTransaction())
      {
        try
        {
          await _session.SaveAsync(produto);
          await transaction.CommitAsync();
        }
        catch (Exception)
        {
          await transaction.RollbackAsync();
          throw;
        }
      }
      return _mapper.Map<ProdutoDTO>(produto);
    }
  }
}

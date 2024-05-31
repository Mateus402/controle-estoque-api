using AutoMapper;
using ControleEstoqueApi.Entities;
using ControleEstoqueApi.IRepositories;
using NHibernate;
using NHibernate.Linq;
using System.Threading.Tasks;

namespace ControleEstoqueApi.Repositories
{
  public class ProdutoRepository : IProdutoRepository
  {
    private readonly NHibernate.ISession _session;
    private readonly IMapper _mapper;

    public ProdutoRepository(NHibernate.ISession session, IMapper mapper)
    {
      _session = session;
      _mapper = mapper;
    }

    public async Task<bool> Existe(Produto entity)
    {
      if (entity == null || string.IsNullOrWhiteSpace(entity.Nome))
      {
        throw new ArgumentException("Produto inválido ou nome não fornecido.");
      }

      // Usando LINQ para consultar o banco de dados
      var existe = await _session.Query<Produto>()
                                 .AnyAsync(p => p.Nome == entity.Nome);

      return existe;
    }
  }
}

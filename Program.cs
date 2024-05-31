using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using ControleEstoqueApi.Mappings;
using ControleEstoqueApi.Services;
using ControleEstoqueApi.Profiles;
using ControleEstoqueApi.IRepositories;
using ControleEstoqueApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ISessionFactory>(factory =>
{
  try
  {
    return Fluently.Configure()
        .Database(PostgreSQLConfiguration.Standard
            .ConnectionString("Host=localhost;Port=5432;Username=postgres;Password=1234;Database=postgres;")
            .ShowSql())
        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProdutoMap>())
        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
        .BuildSessionFactory();
  }
  catch (Exception ex)
  {
    Console.WriteLine("NHibernate configuration error: " + ex.Message);
    throw;
  }
});

builder.Services.AddScoped(factory =>
{
  var sessionFactory = factory.GetService<ISessionFactory>();
  return sessionFactory.OpenSession();
});

// Configurar AutoMapper
//builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(CadastroProfile).Assembly);
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Registrar serviços
builder.Services.AddScoped<ProdutoService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

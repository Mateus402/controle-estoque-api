using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

public class NHibernateHelper
{
  private static ISessionFactory _sessionFactory;

  public static ISessionFactory SessionFactory
  {
    get
    {
      if (_sessionFactory == null)
      {
        InitializeSessionFactory();
      }
      return _sessionFactory;
    }
  }

  private static void InitializeSessionFactory()
  {
    _sessionFactory = Fluently.Configure()
        .Database(PostgreSQLConfiguration.Standard
            .ConnectionString(cs => cs
                .Host("localhost")
                .Port(5432)
                .Database("polls")
                .Username("docker")
                .Password("docker"))
            .ShowSql())
        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateHelper>())
        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
        .BuildSessionFactory();
  }

  public static NHibernate.ISession OpenSession()
  {
    return SessionFactory.OpenSession();
  }
}

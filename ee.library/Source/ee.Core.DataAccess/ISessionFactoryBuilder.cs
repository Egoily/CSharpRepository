using NHibernate;

namespace ee.Core.DataAccess
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory CreateSessionFactory();
        void Build(params string[] args);
    }
}

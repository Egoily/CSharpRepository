using NHibernate;

namespace ee.Core.NhDataAccess
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory CreateSessionFactory();
        void Build(params string[] args);
    }
}

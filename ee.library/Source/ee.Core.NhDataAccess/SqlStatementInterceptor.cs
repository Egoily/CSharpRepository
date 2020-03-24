using NHibernate;

namespace ee.Core.NhDataAccess
{
    public class SqlStatementInterceptor : EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            System.Diagnostics.Trace.WriteLine(sql.ToString());
            return sql;
        }
    }
}

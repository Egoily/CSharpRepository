using NHibernate;

namespace ee.Core.DataAccess
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

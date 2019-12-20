using ee.iLawyer.Db.Entities;
using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Mappings
{
    public class ClientPropertyMap : ClassMap<ClientProperty>
    {
        public ClientPropertyMap()
        {
            Table("ClientProperties");
            LazyLoad();

            Id(x => x.Id);
            //.GeneratedBy.Assigned();   
            Map(x => x.Value);
            Map(x => x.OrderNo);
            Map(x => x.CreateTime);
            Map(x => x.UpdateTime);

            References(x => x.Client).Column("ClientId").Cascade.SaveUpdate().NotFound.Ignore();
            References(x => x.Picker).Column("PropertyPickId").Cascade.SaveUpdate().NotFound.Ignore();
        }
    }
}

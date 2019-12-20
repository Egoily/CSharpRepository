using ee.iLawyer.Db.Entities;
using FluentNHibernate.Mapping;
namespace ee.iLawyer.Db.Mappings
{
    public class ProjectClientMap : ClassMap<ProjectClient>
    {
        public ProjectClientMap()
        {
            Table("ProjectClients");
            LazyLoad();

            Id(x => x.Id);
            //.GeneratedBy.Assigned();
            Map(x => x.OrderNo);
            Map(x => x.CreateTime);
            References(x => x.InProject).Column("ProjectId").NotFound.Ignore();
            References(x => x.Client).Column("ClientId").Cascade.SaveUpdate().NotFound.Ignore();

        }
    }
}

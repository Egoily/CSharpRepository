using ee.Core.Framework.Schema;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;

namespace ee.iLawyer.Ops.Contact.Interfaces
{
    public interface IFoundation
    {
        BaseQueryResponse<Area> GetAreas(GetAreasRequest request);

        BaseQueryResponse<Picker> GetPickers(GetPickersRequest request);

    }
}

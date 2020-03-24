using ee.Core.Framework.Schema;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;

namespace ee.iLawyer.Ops.Contact.Interfaces
{
    public interface IFoundation
    {
        QueryResponse<Area> GetAreas(GetAreasRequest request);

        QueryResponse<Picker> GetPickers(GetPickersRequest request);

    }
}

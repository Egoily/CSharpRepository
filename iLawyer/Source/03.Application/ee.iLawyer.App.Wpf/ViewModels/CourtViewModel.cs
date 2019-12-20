using ee.Core.ComponentModel;
using ee.Core.Framework.Schema;
using ee.iLawyer.App.Wpf.ViewModels.Base;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.AutoMapper;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using ee.iLawyer.ServiceProvider;
using PropertyChanged;

namespace ee.iLawyer.App.Wpf.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    [Ioc(null, false, true)]
    public class CourtViewModel : DataManipulationViewModel<Court>
    {
        private ILawyerServiceProvider serviceProvider;
        public override string PermissionCodePrefix => "root.court.";

        public CourtViewModel()
        {
            serviceProvider = new ILawyerServiceProvider();
        }

        public override BaseQueryResponse<Court> Query()
        {
            return serviceProvider.QueryCourt(new QueryCourtRequest() { Name = SearchText });
        }

        public override BaseResponse Create()
        {
            var request = DtoConverter.Mapper.Map<CreateCourtRequest>(TreadObject);
            return serviceProvider.CreateCourt(request);
        }
        public override BaseResponse Update()
        {
            var request = DtoConverter.Mapper.Map<UpdateCourtRequest>(TreadObject);
            return serviceProvider.UpdateCourt(request);
        }

        public override BaseResponse Remove()
        {
            return serviceProvider.RemoveCourt(new RemoveCourtRequest()
            {
                Ids = new int[] { TreadObject.Id },

            });
        }

    }
}

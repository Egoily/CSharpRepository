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
    public class ClientViewModel : DataManipulationViewModel<Client>
    {
        private ILawyerServiceProvider serviceProvider;

        public override string PermissionCodePrefix => "root.client.";

        public ClientViewModel()
        {
            serviceProvider = new ILawyerServiceProvider();
        }
        public override BaseQueryResponse<Client> Query()
        {
            Cacher.UpdateClients();

            return serviceProvider.QueryClient(new QueryClientRequest() { Name = SearchText });

        }

        public override BaseResponse Create()
        {
            var request = DtoConverter.Mapper.Map<CreateClientRequest>(TreadObject);
            return serviceProvider.CreateClient(request);
        }
        public override BaseResponse Update()
        {
            var request = DtoConverter.Mapper.Map<UpdateClientRequest>(TreadObject);
            return serviceProvider.UpdateClient(request);
        }

        public override BaseResponse Remove()
        {
            return serviceProvider.RemoveClient(new RemoveClientRequest()
            {
                Ids = new int[] { TreadObject.Id },

            });
        }



    }
}

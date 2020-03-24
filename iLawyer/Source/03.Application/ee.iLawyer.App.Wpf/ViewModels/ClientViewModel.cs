using ee.Core.ComponentModel;
using ee.Core.Framework.Schema;
using ee.Core.Net;
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
        public override QueryResponse<Client> Query()
        {
            Cacher.UpdateClients();

            return serviceProvider.QueryClient(new QueryClientRequest() { Name = SearchText });

        }

        public override ResponseBase Create()
        {
            var request = DtoConverter.Mapper.Map<CreateClientRequest>(TreadObject);
            return serviceProvider.CreateClient(request);
        }
        public override ResponseBase Update()
        {
            var request = DtoConverter.Mapper.Map<UpdateClientRequest>(TreadObject);
            return serviceProvider.UpdateClient(request);
        }

        public override ResponseBase Remove()
        {
            return serviceProvider.RemoveClient(new RemoveClientRequest()
            {
                Ids = new int[] { TreadObject.Id },

            });
        }



    }
}

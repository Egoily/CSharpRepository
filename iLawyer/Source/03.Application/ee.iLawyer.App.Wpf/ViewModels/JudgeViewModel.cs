using ee.Core.ComponentModel;
using ee.Core.Framework.Schema;
using ee.iLawyer.App.Wpf.ViewModels.Base;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.AutoMapper;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using ee.iLawyer.ServiceProvider;
using PropertyChanged;
using System.Collections.ObjectModel;
namespace ee.iLawyer.App.Wpf.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    [Ioc(null, false, true)]
    public class JudgeViewModel : DataManipulationViewModel<Judge>
    {
        private ILawyerServiceProvider serviceProvider;

        public override string PermissionCodePrefix => "root.judge.";


        public ObservableCollection<Court> Courts { get; protected set; }


        public JudgeViewModel()
        {
            Courts = Cacher.Courts;
            serviceProvider = new ILawyerServiceProvider();
        }

        public override BaseQueryResponse<Judge> Query()
        {
            return serviceProvider.QueryJudge(new QueryJudgeRequest() { Name = SearchText });
        }

        public override BaseResponse Create()
        {
            var request = DtoConverter.Mapper.Map<CreateJudgeRequest>(TreadObject);
            return serviceProvider.CreateJudge(request);
        }
        public override BaseResponse Update()
        {
            var request = DtoConverter.Mapper.Map<UpdateJudgeRequest>(TreadObject);
            return serviceProvider.UpdateJudge(request);
        }

        public override BaseResponse Remove()
        {
            return serviceProvider.RemoveJudge(new RemoveJudgeRequest()
            {
                Ids = new int[] { TreadObject.Id },

            });
        }



    }
}

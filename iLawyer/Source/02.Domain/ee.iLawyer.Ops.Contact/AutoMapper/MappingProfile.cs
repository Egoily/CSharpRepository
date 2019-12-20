using AutoMapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CourtRank, string>().ConvertUsing(src => src.ToString());
            CreateMap(typeof(List<>), typeof(ObservableCollection<>));
            CreateMap(typeof(ObservableCollection<>), typeof(List<>));
        }
    }
}

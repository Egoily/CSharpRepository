using AutoMapper;
using ee.iLawyer.Ops.Contact.AutoMapper;

namespace ee.iLawyer.Ops.Contact.DTO
{
    //[AddINotifyPropertyChangedInterface]
    [AutoMap(typeof(Db.Entities.ClientProperties), TypeConverter = typeof(ClientPropertyItemTypeConverter))]//=        CreateMap<IList<Db.Entities.ClientProperties>, List<DTO.CategoryValue>>().ConvertUsing<ClientPropertyItemTypeConverter>();
    public class CategoryValue
    {
        public int Id { get; set; }
        public int PickerId { get; set; }
        public string PickerName { get; set; }
        public string Value { get; set; }

        public CategoryValue()
        {

        }
        public CategoryValue(int pickerId, string pickerName, string value, int id = 0)
        {
            PickerId = pickerId;
            PickerName = pickerName;
            Value = value;
            Id = id;
        }
    }
}

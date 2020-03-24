using ee.Core.ComponentModel;
using ee.Core.Exceptions;
using ee.Core.Net;

namespace ee.Core.Framework.Schema
{
    public class Request : RequestBase
    {
        public override void Validate()
        {
            System.Reflection.PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (var pInfo in properties)
            {
                if (pInfo.IsDefined(typeof(ValidateAttibute), true))
                {
                    var customAttributes = pInfo.GetCustomAttributes(typeof(ValidateAttibute), true) as ValidateAttibute[];
                    foreach (var attribute in customAttributes)
                    {

                        attribute.InputValue = pInfo.GetValue(this, null);
                        attribute.PropertyName = pInfo.Name;
                        if (!attribute.Validate())
                        {
                            throw new EeException(ErrorCodes.InvalidParameter, attribute.ErrorMessage);
                        }
                    }
                }
            }

        }
    }
    public class PageRequest : Request
    {
        public virtual int PageIndex { get; set; }
        public virtual int PageSize { get; set; }
    }
    public class IdRequest : Request
    {
        public virtual int Id { get; set; }
    }
    public class IdRequest<T> : Request
    {
        public virtual T Id { get; set; }
    }
}

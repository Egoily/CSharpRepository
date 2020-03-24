namespace ee.Core.Net
{
    public class ResponseBase
    {
        public virtual int Code { get; set; }
        public virtual string Message { get; set; }

        public virtual bool IsOk()
        {
            return Code == 0;
        }
    }

}

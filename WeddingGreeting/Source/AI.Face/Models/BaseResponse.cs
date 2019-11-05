namespace Baidu.AI.Face.Models
{
    public class BaseResponse<T>
    {
        /// <summary>
        ///
        /// </summary>
        public int error_code { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string error_msg { get; set; }

        /// <summary>
        ///
        /// </summary>
        public long log_id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int cached { get; set; }

        /// <summary>
        ///
        /// </summary>
        public T result { get; set; }
    }

    public class BaseResponse : BaseResponse<object>
    { }
}
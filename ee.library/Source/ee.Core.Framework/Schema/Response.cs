using ee.Core.Exceptions;
using ee.Core.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ee.Core.Framework.Schema
{

    public class DataResponse : ResponseBase
    {
        [JsonProperty("Data")]
        public virtual string Data { get; protected set; }

        private object dataObject;
        [JsonIgnore]
        public virtual object DataObject
        {
            get
            {
                return dataObject;
            }
            set
            {
                dataObject = value;
                if (dataObject != null)
                {
                    Data = JsonConvert.SerializeObject(DataObject);
                }
            }
        }

    }

    public class ObjectResponse<T> : ResponseBase
    {
        public virtual T Object { get; set; }


    }
    public class QueryResponse<T> : ResponseBase
    {
        public virtual int? Total { get; set; }

        public virtual IList<T> QueryList { get; set; }



    }

    public static class Converters
    {
        public static DataResponse ToDataResponse<T>(this ObjectResponse<T> source)
        {
            return new DataResponse()
            {
                Code = source.Code,
                Message = source.Message,
                DataObject = source.Object,
            };
        }
        public static ObjectResponse<T> ToObjectResponse<T>(this DataResponse source)
        {
            return new ObjectResponse<T>()
            {
                Code = source?.Code ?? -1,
                Message = source?.Message ?? null,
                Object = JsonConvert.DeserializeObject<T>(source?.Data ?? "")
            };
        }
        public static DataResponse ToDataResponse<T>(this QueryResponse<T> source)
        {
            return new DataResponse()
            {
                Code = source.Code,
                Message = source.Message,
                DataObject = (source.Total, source.QueryList),
            };
        }
        public static QueryResponse<T> ToQueryResponse<T>(this DataResponse source)
        {
            var tuple = JsonConvert.DeserializeObject<Tuple<IList<T>, int?>>(source.Data);
            return new QueryResponse<T>()
            {
                Code = source.Code,
                Message = source.Message,
                QueryList = tuple.Item1,
                Total = tuple.Item2,
            };
        }

        public static DataResponse ToDataResponse(this object source)
        {

            var type = source.GetType();
            if (type.Name == typeof(ObjectResponse<>).Name)
            {
                dynamic res = source;
                return new DataResponse()
                {
                    Code = res.Code,
                    Message = res.Message,
                    DataObject = res.Object,
                };
            }
            else if (type.Name == typeof(QueryResponse<>).Name)
            {
                dynamic res = source;
                return new DataResponse()
                {
                    Code = res.Code,
                    Message = res.Message,
                    DataObject = (res.QueryList, res.Total),
                };
            }
            else if (source is ResponseBase)
            {
                var res = source as ResponseBase;
                return new DataResponse()
                {
                    Code = res.Code,
                    Message = res.Message,
                    DataObject = null,
                };
            }
            else
            {
                throw new EeException(ErrorCodes.DevelopError, "Can not convert to Response type");
            }

        }
    }


}

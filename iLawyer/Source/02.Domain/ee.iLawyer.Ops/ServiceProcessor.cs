using ee.Core.Framework.Processor;
using ee.Core.Logging;
using ee.Core.NhDataAccess;
using System.Reflection;
using ee.Core.Framework.Schema;
using ee.Core.Net;

namespace ee.iLawyer.Ops
{
    public class ServiceProcessor
    {
        public static ApiProcessor<T, TR> CreateProcessor<T, TR>(MethodBase methodBase, dynamic request, bool parameterRequired = true)
             where T : RequestBase
             where TR : ResponseBase, new()
        {
            var processor = new ApiProcessor<T, TR>(methodBase);

            processor.Input(request, parameterRequired);
            //processor.Inbound(() => { SessionManager.GetConnection(); });
            processor.Outbound(() => { SessionManager.CloseConnection(); });
            return processor;
        }

        public static ApiProcessor<T, TR> CreateProcessor<T, TR>(MethodBase methodBase)
            where T : RequestBase
            where TR : ResponseBase, new()
        {
            var processor = new ApiProcessor<T, TR>(methodBase);
            return processor;
        }

    }

    public class ApiProcessor<TRequest, TResponse> : ProcessorInternal<TRequest, TResponse>
             where TRequest : RequestBase
             where TResponse : ResponseBase, new()
    {

        public ApiProcessor(MethodBase method)
        {
            MethodBase = method;
        }
        public override void Debug(object message)
        {
            Logger.Debug(message);
        }

        public override void Error(object message)
        {
            Logger.Error(message);
        }

        public override void Fatal(object message)
        {
            Logger.Fatal(message);
        }

        public override void Info(object message)
        {
            Logger.Info(message);
        }

        public override void Warn(object message)
        {
            Logger.Warn(message);
        }
    }

}

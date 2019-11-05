namespace ee.Core.Behavior
{
    public abstract class Logging
    {

        public abstract void Debug(object message);

        public abstract void Info(object message);
        public abstract void Warn(object message);
        public abstract void Error(object message);

        public abstract void Fatal(object message);

    }
}

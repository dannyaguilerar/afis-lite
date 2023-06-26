namespace AfisLite.Broker.Core.Shared
{
    public abstract class BaseResponse
    {
        public abstract bool IsSuccess { get; }
        public abstract string Message { get; }
    }
}

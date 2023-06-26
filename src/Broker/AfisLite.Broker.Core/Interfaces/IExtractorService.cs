namespace AfisLite.Broker.Core.Interfaces
{
    public interface IExtractorService
    {
        ExtractorResponse ExtractTemplate(byte[] image);
    }

    public abstract class ExtractorResponse
    { 
        public abstract bool IsSuccess { get; }
        public abstract byte[] Template { get; }
    }

}

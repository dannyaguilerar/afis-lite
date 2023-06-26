namespace AfisLite.Broker.Core.Interfaces
{
    public interface IMatcherService
    {
        MatcherResponse MatchImages(byte[] probeImage, byte[] candidateImage);

        MatcherResponse MatchTemplates(byte[] probeTemplate, byte[] candidateTemplate);
    }

    public abstract class MatcherResponse
    {
        public abstract bool IsSuccess { get; }
        public abstract bool IsMatch { get; }
        public abstract double Score { get; }
    }
}

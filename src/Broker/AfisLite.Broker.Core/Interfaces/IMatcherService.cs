using AfisLite.Broker.Core.FingerprintAggregate.Models;

namespace AfisLite.Broker.Core.Interfaces
{
    public interface IMatcherService
    {
        MatcherResponse MatchImages(byte[] probeImage, byte[] candidateImage);

        MatcherResponse MatchTemplates(byte[] probeTemplate, byte[] candidateTemplate);

        MatcherResponse MatchFingerprints(IEnumerable<FingerprintRecord> probe, IEnumerable<FingerprintRecord> candidate);
    }

    public abstract class MatcherResponse
    {
        public abstract bool IsSuccess { get; }
        public abstract bool IsMatch { get; }
        public abstract double Score { get; }
    }
}

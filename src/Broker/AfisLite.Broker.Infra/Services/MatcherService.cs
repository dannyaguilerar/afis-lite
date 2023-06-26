using AfisLite.Broker.Core.Interfaces;
using SourceAFIS;

namespace AfisLite.Broker.Infra.Services
{
    public class MatcherService : IMatcherService
    {
        private readonly double threshold = 40;
        public MatcherService()
        {
            
        }

        public MatcherResponse MatchImages(byte[] probe, byte[] candidate)
        {
            throw new NotImplementedException();
        }

        public MatcherResponse MatchTemplates(byte[] probe, byte[] candidate)
        {
            var pt = new FingerprintTemplate(probe);
            var bt = new FingerprintTemplate(candidate);

            var matcher = new FingerprintMatcher(pt);
            double score = matcher.Match(bt);
            return new MatcherSuccessResponse(score >= threshold, score);
        }
    }

    public class MatcherSuccessResponse : MatcherResponse
    {
        private readonly bool _isMatch;
        private readonly double _score;

        public override bool IsSuccess => true;

        public override bool IsMatch => _isMatch;

        public override double Score => _score;

        public MatcherSuccessResponse(bool isMatch, double score)
        {
            _isMatch = isMatch;
            _score = score;
        }
    }
}

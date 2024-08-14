using AfisLite.Broker.Core.FingerprintAggregate.Models;
using AfisLite.Broker.Core.Interfaces;
using Microsoft.Extensions.Logging;
using SourceAFIS;

namespace AfisLite.Broker.Infra.Services
{
    public class MatcherService : IMatcherService
    {
        private readonly ILogger _logger;
        private readonly double threshold = 40;
        private readonly IExtractorService _extractorService;

        public MatcherService(
            ILogger<MatcherService> logger,
            IExtractorService extractorService)
        {
            _logger = logger;
            _extractorService = extractorService;
        }

        public MatcherResponse MatchImages(byte[] probe, byte[] candidate)
        {
            var probeTemplate = _extractorService.ExtractTemplate(probe).Template;
            var candidateTemplate = _extractorService.ExtractTemplate(candidate).Template;
            var response = MatchTemplates(probeTemplate, candidateTemplate);
            return response;
        }

        public MatcherResponse MatchTemplates(byte[] probe, byte[] candidate)
        {
            _logger.LogInformation("Started template match");
            var pt = new FingerprintTemplate(probe);
            var bt = new FingerprintTemplate(candidate);
            
            var matcher = new FingerprintMatcher(pt);
            double score = matcher.Match(bt);
            var response = new MatcherSuccessResponse(score >= threshold, score);
            _logger.LogInformation($"Finished with score {score}");
            return response;
        }

        public MatcherResponse MatchProbeTemplateWithCandidateTemplates(byte[] probe, IEnumerable<byte[]> candidateTemplates)
        {
            FingerprintMatcher matcher;
            List<double> scores = [];
            foreach (var candidate in candidateTemplates)
            {
                var pt = new FingerprintTemplate(probe);
                matcher = new FingerprintMatcher(pt);
                var bt = new FingerprintTemplate(candidate);
                scores.Add(matcher.Match(bt));
            }
            double score = scores.Max();
            return new MatcherSuccessResponse(score >= threshold, score);
        }

        public MatcherResponse MatchFingerprints(IEnumerable<FingerprintRecord> probe, IEnumerable<FingerprintRecord> candidate)
        {
            FingerprintMatcher matcher;
            List<double> scores = [];
            foreach (var record in probe)
            {
                var pt = new FingerprintTemplate(record.Template);
                matcher = new FingerprintMatcher(pt);

                var fp = candidate.FirstOrDefault(c => c.Type == record.Type);
                
                if (fp != null)
                {
                    var bt = new FingerprintTemplate(fp.Template);
                    scores.Add(matcher.Match(bt));
                }
            }
            double score = scores.Average();
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

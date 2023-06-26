using AfisLite.Broker.Core.Interfaces;
using SourceAFIS;

namespace AfisLite.Broker.Infra.Services
{
    public class ExtractorService : IExtractorService
    {
        
        public ExtractorResponse ExtractTemplate(byte[] image)
        {
            var fingerprintImage = new FingerprintImage(image);
            var fingerprintTemplate = new FingerprintTemplate(fingerprintImage);
            return new SuccessExtractorResponse(fingerprintTemplate.ToByteArray());
        }

    }

    public class SuccessExtractorResponse : ExtractorResponse
    {
        private readonly byte[] _template;
        public override bool IsSuccess => true;

        public override byte[] Template => _template;

        public SuccessExtractorResponse(byte[] template)
        {
            _template = template;
        }
    }

}

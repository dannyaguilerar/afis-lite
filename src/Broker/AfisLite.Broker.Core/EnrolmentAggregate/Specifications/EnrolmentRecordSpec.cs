using AfisLite.Broker.Core.EnrolmentAggregate.Models;
using AfisLite.Broker.Core.FingerprintAggregate.Models;
using Ardalis.Specification;

namespace AfisLite.Broker.Core.EnrolmentAggregate.Specifications
{
    public class EnrolmentRecordSpec : Specification<Enrolment, EnrolmentRecord>
    {
        public EnrolmentRecordSpec()
        {
            
            Query.Where(e => e.Status == EnrolmentStatus.Principal)
                .Include(e => e.Fingerprints);
            Query.Select(e => new EnrolmentRecord
            {
                EnrolmentId = e.Id,
                PersonId = e.PersonId,
                Fingerprints = e.Fingerprints.Select(fp => new FingerprintRecord 
                { 
                    Type = fp.Type,
                    Template = fp.Template
                })
            });
            
        }
    }
}

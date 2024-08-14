using AfisLite.Broker.Core.EnrolmentAggregate;
using AfisLite.Broker.Core.EnrolmentAggregate.Models;
using AfisLite.Broker.Core.FingerprintAggregate.Models;
using Ardalis.Specification;

namespace AfisLite.Broker.Core.VerifyAggregate.Specifications
{
    public class GetEnrolmentRecordByPersonIdSpec : Specification<Enrolment, EnrolmentRecord>
    {
        public GetEnrolmentRecordByPersonIdSpec(int personId)
        {
            Query.Where(e => e.PersonId == personId)
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

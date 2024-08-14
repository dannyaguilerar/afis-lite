using AfisLite.Broker.Core.Shared;

namespace AfisLite.Broker.Core.VerifyAggregate.Models;

public abstract class VerificationResponse : BaseResponse
{
    public abstract bool IsMatch { get; }
    public abstract double Score { get; }
    public abstract int PersonId { get; }
}

public class VerificationSuccessResponse(bool isMatch, double score, int personId) : VerificationResponse
{
    public override bool IsSuccess => true;
    public override bool IsMatch => isMatch;
    public override double Score => score;
    public override int PersonId => personId;

    public override string Message => $"Verification {(IsMatch ? "succeeded" : "failed")} with score {score}";
}


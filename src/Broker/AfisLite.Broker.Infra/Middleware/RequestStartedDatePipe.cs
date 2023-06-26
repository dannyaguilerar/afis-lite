using AfisLite.Broker.Core.Shared;
using MediatR;

namespace AfisLite.Broker.Infra.Middleware
{
    public class RequestStartedDatePipe<TIn, TOut> : IPipelineBehavior<TIn, TOut>
    {

        public Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
        {
            if (request is BaseRequest br)
            {
                br.StartedDate = DateTime.UtcNow;
            }
            return next();
        }
    }
}

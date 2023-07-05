using AfisLite.Broker.Core.Interfaces;
using AfisLite.Broker.Infra.Data;
using AfisLite.Broker.Infra.Middleware;
using AfisLite.Broker.Infra.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AfisLite.Broker.Infra
{
    public static class Configuration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AfisLiteDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));

            services.AddScoped<IMatcherService,MatcherService>();
            services.AddScoped<IExtractorService,ExtractorService>();
                
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestStartedDatePipe<,>));

            return services;
        }
    }
}

using Application.Common.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjectionHelper
{
    public static IServiceCollection InjectApplicationLayer(this IServiceCollection services) =>
        services
            .AddMediatR(configuration => configuration
                .RegisterServicesFromAssembly(typeof(BaseGetAllQuery<>).Assembly));
}
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjectionHelper
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services
            .AddMediatR(configuration => configuration
                .RegisterServicesFromAssembly(typeof(DependencyInjectionHelper).Assembly));

        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingListSample.Infrastructure.Extensions;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}
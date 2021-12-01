using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AdventOfCode.Common.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAdventOfCodeDays(this IServiceCollection serviceCollection)
    {
        var types = DayHelper.GetDaysFromAssemblies();
        types.ForEach(type =>
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(IDay), type, ServiceLifetime.Singleton);
            serviceCollection.TryAddEnumerable(serviceDescriptor);
        });
        return serviceCollection;
    }
}

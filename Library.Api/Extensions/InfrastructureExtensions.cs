using Library.Domain.Aggregates.Book;
using Library.Infrastructure;

namespace Library.Api.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services) => services
            .AddScoped<IBookRepo, BookRepo>();
    }
}

using ETicaret.Baskets.Repositories.Interface;

namespace ETicaret.Baskets.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRepository(this IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<IBasketRepository>();
            return app;
        }
    }
}

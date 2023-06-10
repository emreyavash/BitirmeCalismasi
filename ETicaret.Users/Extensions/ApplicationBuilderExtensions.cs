using ETicaret.Users.Consumers;

namespace ETicaret.Users.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static EventBusUserCreateConsumer Listener { get; set; }
        public static IApplicationBuilder UseEventListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusUserCreateConsumer>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            Listener.Consume();
        }
        private static void OnStopping()
        {
            Listener.Disconnect();
        }
    }
    
}

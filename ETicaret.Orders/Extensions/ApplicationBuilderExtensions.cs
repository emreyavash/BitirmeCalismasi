﻿using ETicaret.Orders.Consumers;

namespace ETicaret.Orders.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static EventBusOrderCreateConsumer Listener { get; set; }
        public static IApplicationBuilder UseEventBusListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusOrderCreateConsumer>();
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

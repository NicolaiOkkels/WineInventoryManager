using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
public class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<WineUpdateConsumer>();

            cfg.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("wine_updates", e =>
                {
                    e.ConfigureConsumer<WineUpdateConsumer>(context);
                });
            });
        });

        services.AddMassTransitHostedService();

        var serviceProvider = services.BuildServiceProvider();

        var busControl = serviceProvider.GetRequiredService<IBusControl>();

        await busControl.StartAsync();

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        await busControl.StopAsync();
    }
}
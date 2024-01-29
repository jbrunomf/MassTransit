using Consumer;
using Consumer.Events;
using Core;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        var nomeFila = configuration.GetSection("MassTransit").GetValue<string>("QueueName") ?? string.Empty;
        var servidor = configuration.GetSection("MassTransit").GetValue<string>("ServerName") ?? string.Empty;
        var username = configuration.GetSection("MassTransit").GetValue<string>("UserName") ?? string.Empty;
        var password= configuration.GetSection("MassTransit").GetValue<string>("Password") ?? string.Empty;

        services.AddHostedService<Worker>();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(servidor, "/", h =>
                {
                    h.Username(username);
                    h.Password(password);
                });

                cfg.ReceiveEndpoint(nomeFila, e =>
                {
                    e.Consumer<PedidoCriadoConsumidor>();
                });

                cfg.ConfigureEndpoints(context);
            });

            x.AddConsumer<PedidoCriadoConsumidor>();
        });
    })
    .Build();

host.Run();

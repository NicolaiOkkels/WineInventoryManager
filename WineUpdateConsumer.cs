using System;
using System.Threading.Tasks;
using MassTransit;
using Newtonsoft.Json;

public class WineUpdateConsumer : IConsumer<WineUpdate>
{
    public async Task Consume(ConsumeContext<WineUpdate> context)
    {
        try
        {
            var wineUpdate = context.Message;
            Console.WriteLine("Received wine update: {0}", JsonConvert.SerializeObject(wineUpdate));
            // Process the wine update as needed
        }
        catch (Exception ex)
        {
            // Log the error or take other appropriate action
            Console.WriteLine("Error processing wine update: {0}", ex.Message);
        }
    }
}
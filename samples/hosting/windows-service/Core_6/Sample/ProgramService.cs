using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using NServiceBus;

class ProgramService :
    ServiceBase
{
    IEndpointInstance endpointInstance;

    #region windowsservice-hosting-main

    static void Main()
    {
        Console.Title = "Samples.WindowsServiceAndConsole";
        using (var service = new ProgramService())
        {
            if (Environment.UserInteractive)
            {
                service.OnStart(null);

                Console.WriteLine("Bus created and configured");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();

                service.OnStop();

                return;
            }
            Run(service);
        }
    }

    #endregion

    #region windowsservice-hosting-onstart

    protected override void OnStart(string[] args)
    {
        AsyncOnStart().GetAwaiter().GetResult();
    }

    async Task AsyncOnStart()
    {
        var endpointConfiguration = new EndpointConfiguration("Samples.WindowsServiceAndConsole");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        endpointConfiguration.EnableInstallers();
        endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        // run any startup actions on the bus
        var myMessage = new MyMessage();
        await endpointInstance.SendLocal(myMessage)
            .ConfigureAwait(false);
    }

    #endregion

    #region windowsservice-hosting-onstop

    protected override void OnStop()
    {
        endpointInstance?.Stop().GetAwaiter().GetResult();
    }

    #endregion

}
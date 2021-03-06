using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;

public class PlaceOrderHandler :
    IHandleMessages<PlaceOrder>
{
    static ILog log = LogManager.GetLogger<PlaceOrderHandler>();

    public Task Handle(PlaceOrder message, IMessageHandlerContext context)
    {
        log.Info($"Received order {message.OrderId} for ${message.Value}.");

        return context.Publish(new OrderAccepted
        {
            OrderId = message.OrderId
        });
    }
}
using MassTransit;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    //public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger)
    public class OrderCreatedEventHandler(
        ILogger<OrderCreatedEventHandler> logger,
        IPublishEndpoint publishEndpoint)
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

            var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);

            //if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            //{
            //    var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
            //    await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            //}
        }
    }
}

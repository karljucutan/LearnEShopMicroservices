namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketRequest(string UserName);
    public record DeleteBasketResponse(bool IsSuccess);

    public class DeleteBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket",
                async (DeleteBasketRequest request, ISender sender) =>
                {
                    var command = request.Adapt<DeleteBasketCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<DeleteBasketResponse>();

                    return Results.Ok(response);
                })
                .WithName("DeleteBasket")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Basket")
                .WithDescription("Delete Basket");
        }
    }
}

using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommand : IRequest 
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}

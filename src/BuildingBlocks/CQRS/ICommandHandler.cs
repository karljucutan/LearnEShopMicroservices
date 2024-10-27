using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommandHandler<in TQuery>
        : IRequestHandler<TQuery>
        where TQuery : ICommand
    {
    }

    public interface ICommandHandler<in TCommand, TResponse> 
        : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
    }
}

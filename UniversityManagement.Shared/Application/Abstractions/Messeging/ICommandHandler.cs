using MediatR;

namespace UniversityManagement.Shared.Application.Abstractions.Messeging
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
    }
}

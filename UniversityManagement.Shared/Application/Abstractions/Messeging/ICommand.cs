using MediatR;

namespace UniversityManagement.Shared.Application.Abstractions.Messeging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}

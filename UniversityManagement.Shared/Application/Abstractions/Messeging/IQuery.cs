using MediatR;

namespace UniversityManagement.Shared.Application.Abstractions.Messeging
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}

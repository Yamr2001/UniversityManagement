using MediatR;

namespace UniversityManagement.Shared.Application.Abstractions.Messeging
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
    }
}

using AutoMapper;
using Microsoft.Extensions.Logging;
using UniversityManagement.Domain.Base;
using UniversityManagement.Domain.Entities.Users;
using UniversityManagement.Shared.Application.Abstractions.Messeging;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Features.Registration.Quries.GetAllUsersList
{
    public class GetUserListQueryHandler : IQueryHandler<GetUserListQuery, QueryResultResource<GetUserListVm>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserListQueryHandler> _logger;

        public GetUserListQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetUserListQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResultResource<GetUserListVm>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var StudentQuery = _mapper.Map<UserQuery>(request);
                var queryResult = await _uow.UserRepository.GetPagedUserList(StudentQuery, cancellationToken);
                return _mapper.Map<QueryResultResource<GetUserListVm>>(queryResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Users");
                return new();
            }
        }

    }
}

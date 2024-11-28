using FSPBook.Application.DTOs;
using FSPBook.Application.Queries;
using FSPBook.Data.Interfaces;
using MediatR;

namespace FSPBook.Application.Features.Profiles.Queries
{
    public class GetProfilesQueryHandler : IRequestHandler<GetProfilesQuery, List<ProfileDto>>
    {
        private readonly IProfileRepository _profileRepository;

        public GetProfilesQueryHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<List<ProfileDto>> Handle(GetProfilesQuery request, CancellationToken cancellationToken)
        {
            var profiles = await _profileRepository.GetAllAsync();
            return profiles.Select(p => new ProfileDto
            {
                Id = p.Id,
                FullName = $"{p.FirstName} {p.LastName}"
            }).ToList();
        }
    }
}

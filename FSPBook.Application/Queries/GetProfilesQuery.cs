using FSPBook.Application.DTOs;
using MediatR;

namespace FSPBook.Application.Queries
{
    public class GetProfilesQuery : IRequest<List<ProfileDto>>
    {
    }
}

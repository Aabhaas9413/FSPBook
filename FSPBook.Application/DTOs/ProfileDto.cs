using FSPBook.Data.Entities;

namespace FSPBook.Application.DTOs
{
    public class ProfileDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? JobTitle { get; set; }

        public static explicit operator ProfileDto(Profile profile)
        {
            return new ProfileDto
            {
                Id = profile.Id,
                FullName = $"{profile.FirstName} {profile.LastName}",
                JobTitle = profile.JobTitle
            };
        }   
    }
}

using FSPBook.Data.Entities;
using FSPBook.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FSPBook.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly Context _context;

        public ProfileRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Profile>> GetAllAsync()
        {
            return await _context.Profile.ToListAsync();
        }

        public async Task<Profile> GetProfileByIdAsync(int id)
        {
            var profile = await _context.Profile.FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return null;
            }

            return new Profile
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                JobTitle = profile.JobTitle
            };
        }
    }
}

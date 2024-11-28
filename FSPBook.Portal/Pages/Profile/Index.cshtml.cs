using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using FSPBook.Data.Interfaces;
using FSPBook.Application.DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FSPBook.Presentation.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostRepository _postRepository;

        public IndexModel(IProfileRepository profileRepository, IPostRepository postRepository)
        {
            _profileRepository = profileRepository;
            _postRepository = postRepository;
        }

        public ProfileDto Profile { get; set; }
        public List<PostDto> Posts { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Fetch the profile
            var profile = await _profileRepository.GetProfileByIdAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            else
            {
                Profile = (ProfileDto)profile;
            }

            // Fetch the latest posts for the profile
            var posts  = await _postRepository.GetPostsByAuthorIdAsync(id);
            Posts = posts.Select(p => (PostDto)p).ToList();

            return Page();
        }
    }
}

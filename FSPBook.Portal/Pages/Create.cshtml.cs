using FSPBook.Application.Commands;
using FSPBook.Application.DTOs;
using FSPBook.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FSPBook.Presentation.Pages.Create
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<ProfileDto> Profiles { get; set; }

        public bool Success { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Choose a person to post on behalf of")]
        [Range(1, 10000, ErrorMessage = "Choose a person to post on behalf of")]
        public int ProfileId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Write a post")]
        [MinLength(1, ErrorMessage = "Post needs some content")]
        public string ContentInput { get; set; }

        public async Task OnGetAsync()
        {
            // Fetch profiles using a query
            Profiles = await _mediator.Send(new GetProfilesQuery());
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload profiles to re-render the page on validation failure
                Profiles = await _mediator.Send(new GetProfilesQuery());
                return Page();
            }

            // Create a new post using a command
            var command = new CreatePostCommand
            {
                AuthorId = ProfileId,
                Content = ContentInput
            };

            var totalPosts = await _mediator.Send(command);
            if (totalPosts > 0)
            {
                Success = true;
                ContentInput = string.Empty;
                ProfileId = 0;
                ModelState.Clear();
                // Reload profiles after a successful post creation
                Profiles = await _mediator.Send(new GetProfilesQuery());
            }          

            return Page();
        }
    }
}

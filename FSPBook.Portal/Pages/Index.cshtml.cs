using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using FSPBook.Application.DTOs;
using FSPBook.Application.Queries;
using FSPBook.Infrastructure.Services; 
using System;

namespace FSPBook.Presentation.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly NewsService _newsService;

        public IndexModel(IMediator mediator, NewsService newsService)
        {
            _mediator = mediator;
            _newsService = newsService;
        }

        public List<PostDto> Posts { get; set; }
        public List<NewsArticle> NewsHeadlines { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int? currentPage = 1)
        {
            // Fetch posts
            CurrentPage = currentPage ?? 1;
            Console.WriteLine($"Current Page: {CurrentPage}");

            var query = new GetPostsQuery
            {
                Page = CurrentPage,
                PageSize = 10 
            };

            var result = await _mediator.Send(query);
            Posts = result.Posts;
            TotalPages = result.TotalPages;

            Console.WriteLine($"Total Pages: {TotalPages}, Posts Fetched: {Posts.Count}");

            
            NewsHeadlines = await _newsService.GetTopTechnologyHeadlinesAsync(5);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace FSPBook.Infrastructure.Services
{
    public class NewsService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public NewsService(IConfiguration configuration)
        {
            _apiKey = configuration["TheNewsApi:ApiKey"];
            _baseUrl = configuration["TheNewsApi:BaseUrl"];
        }

        public async Task<List<NewsArticle>> GetTopTechnologyHeadlinesAsync(int limit = 5)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest();

            // Add query parameters
            request.Method = Method.Get;
            request.AddQueryParameter("api_token", _apiKey);
            request.AddQueryParameter("categories", "tech");
            request.AddQueryParameter("search", "apple");
            request.AddQueryParameter("limit", limit.ToString());

            // Execute request
            var response = await client.ExecuteAsync<NewsResponse>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                // Handle errors gracefully
                return new List<NewsArticle>();
            }

            return response.Data.Data ?? new List<NewsArticle>();
        }
    }

    public class NewsResponse
    {
        public List<NewsArticle> Data { get; set; }
    }

    public class NewsArticle
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}

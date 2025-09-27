using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsScraper.Models;
using NewsScraper.Services;

namespace NewsScraper.Pages
{
    public class SportModel : PageModel
    {
        private readonly NewsScraperService _scraper;

        public List<NewsArticle> SportNews { get; set; } = new();

        public SportModel(NewsScraperService scraper)
        {
            _scraper = scraper;
        }
        public async Task OnGetAsync()
        {
            SportNews = await _scraper.GetSportNewsAsync();
        }
    }
}

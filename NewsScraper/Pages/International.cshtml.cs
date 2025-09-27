using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsScraper.Models;
using NewsScraper.Services;

namespace NewsScraper.Pages
{
    public class InternationalModel : PageModel
    {
        private readonly NewsScraperService _scraper;
        public List<NewsArticle> InternationalNews { get; set; } = new();

        public InternationalModel(NewsScraperService scraper)
        {
            _scraper = scraper;
        }

        public async Task OnGetAsync()
        {
            InternationalNews = await _scraper.GetInternationalNewsAsync();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsScraper.Models;
using NewsScraper.Services;

namespace NewsScraper.Pages
{
    public class TechModel : PageModel
    {
        private readonly NewsScraperService _scraper;
        public List<NewsArticle> TechNews { get; set; } = new();

        public TechModel(NewsScraperService scraper)
        {
            _scraper = scraper;
        }
        public async Task OnGetAsync()
        {
            TechNews = await _scraper.GetTechNewsAsync();
        }
    }
}
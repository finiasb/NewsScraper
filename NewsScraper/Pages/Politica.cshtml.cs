using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsScraper.Models;
using NewsScraper.Services;

namespace NewsScraper.Pages
{
    public class PoliticaModel : PageModel
    {
        private readonly NewsScraperService _scraper;

        public PoliticaModel(NewsScraperService scraper)
        {
            _scraper = scraper;
        }
        public List<NewsArticle> PoliticsNews { get; set; } = new();

        public async Task OnGetAsync()
        {
            PoliticsNews = await _scraper.GetPoliticsNewsAsync();
        }
    }
}

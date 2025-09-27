using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsScraper.Models;
using NewsScraper.Services;

public class IndexModel : PageModel
{
    private readonly NewsScraperService _scraperService;

    public NewsArticle FirstSportArticle { get; set; }
    public NewsArticle SecondSportArticle { get; set; }
    public NewsArticle ThirdSportArticle { get; set; }
    public NewsArticle FirstTechArticle { get; set; }
    public NewsArticle SecondTechArticle { get; set; }
    public NewsArticle PoliticsArticle { get; set; }
    public NewsArticle InternationalArticle { get; set; }

    public IndexModel(NewsScraperService scraperService)
    {
        _scraperService = scraperService;
    }

    public async Task OnGetAsync()
    {
        var sportTask = _scraperService.GetSportNewsAsync();
        var techTask = _scraperService.GetTechNewsAsync();
        var politicsTask = _scraperService.GetPoliticsNewsAsync();
        var internationalTask = _scraperService.GetInternationalNewsAsync();

        await Task.WhenAll(sportTask, techTask, politicsTask, internationalTask);

        var sportArticles = sportTask.Result.ToList();
        var techArticles = techTask.Result.ToList();

        // Acum este corect - atribuie articole individuale
        FirstSportArticle = sportArticles.ElementAtOrDefault(0);
        SecondSportArticle = sportArticles.ElementAtOrDefault(1);
        ThirdSportArticle = sportArticles.ElementAtOrDefault(2);

        FirstTechArticle = techArticles.ElementAtOrDefault(0);
        SecondTechArticle = techArticles.ElementAtOrDefault(1);

        PoliticsArticle = politicsTask.Result.FirstOrDefault();
        InternationalArticle = internationalTask.Result.FirstOrDefault();
    }
}
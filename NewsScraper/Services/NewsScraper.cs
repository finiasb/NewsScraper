using HtmlAgilityPack;
using NewsScraper.Models;

namespace NewsScraper.Services
{
    public class NewsScraperService
    {
        private readonly HttpClient _httpClient;

        public NewsScraperService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NewsArticle>> GetSportNewsAsync()
        {
            return await ScrapeAsync("https://www.digi24.ro/stiri/sport", "//h2/a");
        }

        public async Task<List<NewsArticle>> GetPoliticsNewsAsync()
        {
            return await ScrapeAsync("https://www.digi24.ro/politica", "//h2/a");
        }

        public async Task<List<NewsArticle>> GetInternationalNewsAsync()
        {
            return await ScrapeAsync("https://www.digi24.ro/stiri/externe", "//h2/a");
        }

        public async Task<List<NewsArticle>> GetTechNewsAsync()
        {
            return await ScrapeAsync("https://www.digi24.ro/stiri/sci-tech", "//h2/a");
        }

        private async Task<List<NewsArticle>> ScrapeAsync(string url, string xpath)
        {
            var response = await _httpClient.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(response);

            var articles = new List<NewsArticle>();
            var baseUri = new Uri(url);

            var nodes = doc.DocumentNode.SelectNodes(xpath);
            if (nodes != null)
            {
                foreach (var node in nodes.Take(21))
                {
                    var link = node.GetAttributeValue("href", "#");
                    if (!link.StartsWith("http"))
                        link = new Uri(baseUri, link).ToString();

                    var imgNode = node.SelectSingleNode(".//img")
                                  ?? node.ParentNode.SelectSingleNode(".//img")
                                  ?? node.SelectSingleNode("ancestor::article//img")
                                  ?? node.SelectSingleNode("ancestor::div[contains(@class,'image')]//img");

                    string imageUrl = "https://via.placeholder.com/400x250?text=No+Image";

                    if (imgNode != null)
                    {
                        imageUrl = imgNode.GetAttributeValue("src", null)
                                  ?? imgNode.GetAttributeValue("data-src", null)
                                  ?? imgNode.GetAttributeValue("data-lazy-src", null)
                                  ?? imgNode.GetAttributeValue("data-original", null);

                        if (!string.IsNullOrEmpty(imageUrl) && !imageUrl.StartsWith("http"))
                        {
                            imageUrl = new Uri(baseUri, imageUrl).ToString();
                        }
                    }

                    var title = node.InnerText.Trim();
                    if (string.IsNullOrEmpty(title))
                    {
                        title = node.GetAttributeValue("title", "Fără titlu");
                    }

                    articles.Add(new NewsArticle
                    {
                        Title = title,
                        Link = link,
                        ImageUrl = imageUrl
                    });
                }
            }

            return articles;
        }
    }
}
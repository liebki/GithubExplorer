using GithubExplorer.Models;
using GithubNet;

namespace GithubExplorer.Services;

public class DataManager
{
    public GithubExplorerSettings ActiveSettings { get; set; } =
        new(StaticServingClass.GithubTrendingBaseUrl, "daily", false);

    public IEnumerable<TrendRepository> LoadedItems { get; set; } = new List<TrendRepository>();
}
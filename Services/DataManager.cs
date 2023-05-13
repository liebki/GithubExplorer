using GithubExplorer.Models;
using GithubNet;

namespace GithubExplorer.Services
{
    public class DataManager
    {
        public GithubExplorerSettings ActiveSettings { get; set; } = new("https://github.com/trending", "daily", false);

        public List<TrendItem> LoadedItems { get; set; } = new();
    }
}
using System;
using System.Linq;
using GithubExplorer.Models;

namespace GithubExplorer.Services
{
    public class DataManager
    {
        public GithubExplorerSettings ActiveSettings { get; set; } = new("https://github.com/trending", "daily", false);

        public List<TrendEntry> LoadedEntries { get; set; } = new();

    }
}
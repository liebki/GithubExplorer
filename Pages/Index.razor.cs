using GithubExplorer.Services;
using GithubNet;
using Microsoft.AspNetCore.Components;

namespace GithubExplorer.Pages
{
    partial class Index
    {
        [Inject]
        public DataManager Datamanager { get; set; }

        [Inject]
        public GithubNetClient TrendMan { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Datamanager.LoadedItems = await Task.Run(() => TrendMan.GetTrendItemsAsync(customQuery: Datamanager.ActiveSettings.ActiveGithubUrl));
        }
    }
}
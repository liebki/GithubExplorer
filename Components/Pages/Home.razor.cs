using GithubExplorer.Services;
using GithubNet;
using Microsoft.AspNetCore.Components;

namespace GithubExplorer.Components.Pages;

partial class Home
{
    [Inject] public DataManager Datamanager { get; set; }

    [Inject] public GithubNetClient TrendMan { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Datamanager.LoadedItems = await Task.Run(() =>
            TrendMan.GetAllTrendingRepositories(Datamanager.ActiveSettings.ActiveGithubUrl));
    }
}
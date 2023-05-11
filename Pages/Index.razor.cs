using System;
using System.Linq;
using GithubExplorer.Services;
using Microsoft.AspNetCore.Components;

namespace GithubExplorer.Pages
{
    partial class Index
    {

        [Inject]
        public DataManager Datamanager { get; set; }

        [Inject]
        public GithubTrendingManager TrendMan { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Datamanager.LoadedEntries = await Task.Run(() => TrendMan.GetAllTrendEntries(Datamanager.ActiveSettings.ActiveGithubUrl));
        }
    }
}

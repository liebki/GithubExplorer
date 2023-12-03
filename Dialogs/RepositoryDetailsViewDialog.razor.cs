using GithubNet;
using Microsoft.AspNetCore.Components;

namespace GithubExplorer.Dialogs
{
    partial class RepositoryDetailsViewDialog
    {
        [Parameter]
        public TrendRepository EntryItem { get; set; } = null;

        private bool ReadmeIsLoaded { get; set; } = false;

        private string ReadMeMarkdownString { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            ReadmeIsLoaded = false;
            (string readmeContent, string _) = await Task.Run(() => EntryItem.GetReadmeAuto());

            ReadMeMarkdownString = readmeContent;
            ReadmeIsLoaded = true;
        }
    }
}
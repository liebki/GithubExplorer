using GithubExplorer.Dialogs;
using GithubExplorer.Services;
using GithubNet;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GithubExplorer.Components
{
    partial class RepositoryView
    {
        [Parameter]
        public TrendItem EntryItem { get; set; }

        [Inject]
        public DataManager Datamanager { get; set; }

        [Inject]
        public GithubNetClient TrendMan { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        private bool IsDetailsLoadingButtonLocked = false;

        private async Task OpenRepositoryDetailsViewDialog()
        {
            DialogOptions options = new()
            {
                CloseButton = true,
                MaxWidth = MaxWidth.False,
                FullWidth = true
            };

            DialogParameters parameters = new() { ["EntryItem"] = EntryItem };
            await DialogService.ShowAsync<RepositoryDetailsViewDialog>(string.Empty, parameters, options);
        }

        private async Task FillRepositoryDetails()
        {
            Snackbar.Add($"Loading details for {EntryItem.RespositoryName}", Severity.Normal);
            IsDetailsLoadingButtonLocked = true;

            TrendItem result = await Task.Run(() => TrendMan.GetTrendItemDetailsAsync(EntryItem));
            EntryItem = result;

            Snackbar.Add($"Details loaded for {EntryItem.RespositoryName}", Severity.Success);
            IsDetailsLoadingButtonLocked = false;
        }

        private string RepositoryTopicsSelectText()
        {
            return $"Repository topics: {EntryItem.Topics.Count()}";
        }
    }
}
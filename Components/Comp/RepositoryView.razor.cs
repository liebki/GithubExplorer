using GithubExplorer.Dialogs;
using GithubExplorer.Services;
using GithubNet;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GithubExplorer.Components.Comp;

partial class RepositoryView
{
    private bool IsDetailsLoadingButtonLocked = false;

    [Parameter] public TrendRepository EntryItemLoad { get; set; }

    [Inject] public DataManager Datamanager { get; set; }

    [Inject] public GithubNetClient TrendMan { get; set; }

    [Inject] public IDialogService DialogService { get; set; }

    [Inject] public ISnackbar Snackbar { get; set; }

    public FullRepository FullRep { get; set; } = null;

    private async Task OpenRepositoryDetailsViewDialog()
    {
        DialogOptions options = new()
        {
            CloseButton = true,
            MaxWidth = MaxWidth.False,
            FullWidth = true
        };

        DialogParameters parameters = new() { ["EntryItem"] = EntryItemLoad };
        await DialogService.ShowAsync<RepositoryDetailsViewDialog>(string.Empty, parameters, options);
    }

    private async Task FillRepositoryDetails()
    {
        Snackbar.Add($"Loading details for {EntryItemLoad.RepositoryName}");
        IsDetailsLoadingButtonLocked = true;

        FullRepository result = await Task.Run(() => TrendMan.GetFullRepository(EntryItemLoad.Url));
        FullRep = result;

        Snackbar.Add($"Details loaded for {EntryItemLoad.RepositoryName}", Severity.Success);
        IsDetailsLoadingButtonLocked = false;
    }

    private string GetLastCommitText()
    {
        return $"'{FullRep.LastCommitText}'";
    }

    private string RepositoryTopicsSelectText()
    {
        return $"Repository topics: {FullRep.Topics.Length}";
    }
}
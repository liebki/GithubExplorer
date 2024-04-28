using GithubExplorer.Dialogs;
using GithubExplorer.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NavigationManagerUtils;

namespace GithubExplorer.Components.Layout;

partial class MainLayout
{
    [Inject] public NavManUtils NavMan { get; set; }

    [Inject] public DataManager Datamanager { get; set; }

    [Inject] public ConfigManager ConfigMan { get; set; }

    [Inject] private IDialogService DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Datamanager.ActiveSettings = ConfigMan.CheckSettings();
    }

    private async Task OpenSearchDialog()
    {
        DialogOptions options = new()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true
        };

        IDialogReference dialog = await DialogService.ShowAsync<SearchDialog>("Search", options);
        DialogResult result = await dialog.Result;

        if (!result.Canceled)
        {
            Tuple<string, string> githubUrlData = (Tuple<string, string>)result.Data;
            if (Uri.IsWellFormedUriString(githubUrlData.Item1, UriKind.Absolute) &&
                githubUrlData.Item1.StartsWith(StaticServingClass.GithubTrendingBaseUrl) &&
                !githubUrlData.Item1.StartsWith($"{StaticServingClass.GithubTrendingBaseUrl}/developers"))
                Datamanager.ActiveSettings.ActiveGithubUrl = githubUrlData.Item1;

            Datamanager.ActiveSettings.ActiveGithubUrlTrendType = githubUrlData.Item2;
            ConfigManager.WriteSettings(Datamanager.ActiveSettings);

            NavMan.Reload();
        }
    }

    private void ChangeDarkmode()
    {
        Datamanager.ActiveSettings.IsDarkmodeEnabled = !Datamanager.ActiveSettings.IsDarkmodeEnabled;
        ConfigManager.WriteSettings(Datamanager.ActiveSettings);
    }
}
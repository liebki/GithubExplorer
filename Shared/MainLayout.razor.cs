using System;
using MudBlazor;
using System.Linq;
using NavigationManagerUtils;
using GithubExplorer.Dialogs;
using GithubExplorer.Services;
using Microsoft.AspNetCore.Components;

namespace GithubExplorer.Shared
{

	partial class MainLayout
	{
		[Inject]
		public NavManUtils NavMan { get; set; }

		[Inject]
		public DataManager Datamanager { get; set; }

		[Inject]
		public ConfigManager ConfigMan { get; set; }

		[Inject]
		IDialogService DialogService { get; set; }

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
				Tuple<string, string> GithubUrlData = (Tuple<string, string>)result.Data;
				if (Uri.IsWellFormedUriString(GithubUrlData.Item1, UriKind.Absolute) && GithubUrlData.Item1.StartsWith("https://github.com/trending") && !GithubUrlData.Item1.StartsWith("https://github.com/trending/developers"))
				{
					Datamanager.ActiveSettings.ActiveGithubUrl = GithubUrlData.Item1;
				}

				Datamanager.ActiveSettings.ActiveGithubUrlTrendType = GithubUrlData.Item2;
				ConfigMan.WriteSettings(Datamanager.ActiveSettings);

				NavMan.Reload();
			}
		}

		private void ChangeDarkmode()
		{
			Datamanager.ActiveSettings.IsDarkmodeEnabled = !Datamanager.ActiveSettings.IsDarkmodeEnabled;
			ConfigMan.WriteSettings(Datamanager.ActiveSettings);
		}
	}
}
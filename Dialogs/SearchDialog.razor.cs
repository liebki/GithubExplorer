using System;
using MudBlazor;
using System.Linq;
using Microsoft.AspNetCore.Components;
using GithubExplorer.Services;

namespace GithubExplorer.Dialogs
{
	partial class SearchDialog
	{
		[CascadingParameter]
		MudDialogInstance MudDialog { get; set; }

		[Inject]
		public DataManager Datamanager { get; set; }

		void Submit() => MudDialog.Close(DialogResult.Ok(new Tuple<string, string>(NewGithubUrl, TrendType)));
		void Cancel() => MudDialog.Cancel();

		private string TrendType { get; set; } = "daily";

		private string NewGithubUrl { get; set; } = string.Empty;

		protected override async Task OnInitializedAsync()
		{
			NewGithubUrl = Datamanager.ActiveSettings.ActiveGithubUrl;
		}

		private void SetTrendType(int trendtype)
		{
			if (trendtype == 0)
			{
				TrendType = "daily";
			}
			else if (trendtype == 1)
			{
				TrendType = "weekly";
			}
			else
			{
				TrendType = "monthly";
			}
			BuildGithubUrl();
		}

		private void BuildGithubUrl()
		{
			if(string.IsNullOrEmpty(NewGithubUrl) || string.IsNullOrWhiteSpace(NewGithubUrl) || !NewGithubUrl.Contains("github.com/trending", StringComparison.InvariantCultureIgnoreCase))
			{
				NewGithubUrl = "https://github.com/trending";
			}

			if (NewGithubUrl.Contains("daily", StringComparison.InvariantCultureIgnoreCase))
			{
				NewGithubUrl = NewGithubUrl.Replace("daily", TrendType);
			}
			else if (NewGithubUrl.Contains("weekly", StringComparison.InvariantCultureIgnoreCase))
			{
				NewGithubUrl = NewGithubUrl.Replace("weekly", TrendType);
			}
			else if (NewGithubUrl.Contains("monthly", StringComparison.InvariantCultureIgnoreCase))
			{
				NewGithubUrl = NewGithubUrl.Replace("monthly", TrendType);
			}
			else
			{
				if (NewGithubUrl.Contains('?'))
				{
					NewGithubUrl += $"&{TrendType}";
				}
				else
				{
					NewGithubUrl += $"?{TrendType}";
				}
			}
		}
	}
}

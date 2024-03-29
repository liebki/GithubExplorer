﻿using GithubExplorer.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GithubExplorer.Dialogs
{
    partial class SearchDialog
    {
        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Inject]
        public DataManager Datamanager { get; set; }

        private void Submit() => MudDialog.Close(DialogResult.Ok(new Tuple<string, string>(NewGithubUrl, TrendType)));

        private void Cancel() => MudDialog.Cancel();

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
            if (string.IsNullOrEmpty(NewGithubUrl) || string.IsNullOrWhiteSpace(NewGithubUrl) || !NewGithubUrl.Contains("github.com/trending", StringComparison.InvariantCultureIgnoreCase))
            {
                NewGithubUrl = StaticServingClass.GithubTrendingBaseUrl;
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
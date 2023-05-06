using System;
using System.Linq;
using NavigationManagerUtils;
using GithubExplorer.Services;
using Microsoft.AspNetCore.Components;

namespace GithubExplorer.Shared
{

	partial class MainLayout
	{
		[Inject]
		public NavManUtils NavMan { get; set; }


		[Inject]
		public DataMan Datamanager { get; set; }

		private string NewGithubUrl { get; set; }

		private void ChangeGithubUrl()
		{
			if (Uri.IsWellFormedUriString(NewGithubUrl, UriKind.Absolute) && NewGithubUrl.StartsWith("https://github.com/trending") && !NewGithubUrl.StartsWith("https://github.com/trending/developers"))
			{
				Datamanager.ActiveGithubUrl = NewGithubUrl;
				ReloadPage();
			}
			else
			{
				NewGithubUrl = string.Empty;
			}
		}

		private void ReloadPage()
		{
			NavMan.Reload();
		}
	}
}

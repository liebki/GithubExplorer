using System;
using System.Linq;
using GithubExplorer.Models;
using GithubExplorer.Services;
using Microsoft.AspNetCore.Components;

namespace GithubExplorer.Pages
{
	partial class Index
	{

		[Inject]
		public DataMan Datamanager { get; set; }

		[Inject]
		public GithubTrendingManager TrendMan { get; set; }

		List<TrendEntry> entries { get; set; } = new();

		protected override async Task OnInitializedAsync()
		{
			entries = TrendMan.GetAllTrendEntries(Datamanager.ActiveGithubUrl);
		}

	}
}

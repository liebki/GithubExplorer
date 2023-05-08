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
		public DataManager Datamanager { get; set; }

		[Inject]
		public GithubTrendingManager TrendMan { get; set; }

		List<TrendEntry> entries { get; set; } = new();

		protected override async Task OnInitializedAsync()
		{
			Tuple<List<TrendEntry>, string> EntriesAndToplanguage = TrendMan.GetAllTrendEntries(Datamanager.ActiveSettings.ActiveGithubUrl);

			entries = EntriesAndToplanguage.Item1;
			Datamanager.MostUsedProgramminglanguage = EntriesAndToplanguage.Item2;
		}

	}
}

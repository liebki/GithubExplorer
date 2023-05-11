using System;
using System.Linq;
using GithubExplorer.Models;
using Microsoft.AspNetCore.Components;

namespace GithubExplorer.Dialogs
{
	partial class RepositoryDetailsViewDialog
	{
		[Parameter]
		public TrendEntry EntryItem { get; set; } = new();

		private string ReadMeMarkdownString { get; set; } = string.Empty;

		protected override async Task OnInitializedAsync()
		{
			using HttpClient client = new();
			try
			{
				ReadMeMarkdownString = await client.GetStringAsync(EntryItem.GetReadMeUrl("main"));
			}
			catch (Exception)
			{
				ReadMeMarkdownString = await client.GetStringAsync(EntryItem.GetReadMeUrl("master"));
			}
		}

	}
}

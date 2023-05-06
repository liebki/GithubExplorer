using System;
using System.Linq;
using GithubExplorer.Models;
using GithubExplorer.Services;
using Microsoft.AspNetCore.Components;

namespace GithubExplorer.Components
{
	partial class Entry
	{
		[Parameter]
		public TrendEntry EntryItem { get; set; }

		[Inject]
		public DataMan Datamanager { get; set; }

	}
}

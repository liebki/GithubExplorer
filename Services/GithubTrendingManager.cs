using System.Net;
using System.Text;
using HtmlAgilityPack;
using GithubExplorer.Models;

namespace GithubExplorer.Services
{
	public class GithubTrendingManager
	{
		public List<TrendEntry> GetAllTrendEntries(string customquery = "https://github.com/trending")
		{
			List<TrendEntry> TrendingRepositoriesList = new();
			HtmlWeb Github = new();

			HtmlDocument GithubDoc = Github.Load(customquery);
			IEnumerable<HtmlNode> nodes = GithubDoc.QuerySelectorAll("article.Box-row");

			foreach (HtmlNode productElement in nodes)
			{
				try
				{
					HtmlNode Description = productElement.QuerySelector("article.Box-row > p");
					HtmlNode Username = productElement.QuerySelector("article.Box-row span.text-normal");

					HtmlNode RepositoryLink = productElement.QuerySelector("article.Box-row > h2 > a");
					IList<HtmlNode> TotalForksAndStars = productElement.QuerySelectorAll("article.Box-row > div > a.Link--muted");

					string DescriptionFiltered = string.Empty;
					if (Description != null)
					{
						DescriptionFiltered = FilterLineBreaks(Description.InnerText);
						DescriptionFiltered = WebUtility.HtmlDecode(DescriptionFiltered);
					}
					string UsernameFiltered = FilterLineBreaks(Username.InnerText.Replace(" /", string.Empty));

					string RepositoryLinkFiltered = $"https://github.com{RepositoryLink.Attributes["href"].Value}";
					string[] RepositoryNameParse = RepositoryLinkFiltered.Split('/');

					string RepositoryName = "An error occured";

					if (RepositoryNameParse?.Length >= 2)
					{
						RepositoryName = RepositoryNameParse[4];
					}

					string TotalStarsFiltered = FilterLineBreaks(TotalForksAndStars[0].InnerText);
					string TotalForksFiltered = FilterLineBreaks(TotalForksAndStars[1].InnerText);

					string ProgrammingLanguage = "None";
					if (productElement.QuerySelectorAll("article.Box-row > div > span > span") != null)
					{
						IList<HtmlNode> ProgrammingLanguageElementCount = productElement.QuerySelectorAll("article.Box-row > div > span > span");
						if (ProgrammingLanguageElementCount.Count > 0)
						{
							ProgrammingLanguage = ProgrammingLanguageElementCount[1].InnerText;
						}
					}

					TrendingRepositoriesList.Add(new(UsernameFiltered, RepositoryLinkFiltered, RepositoryName, DescriptionFiltered, TotalStarsFiltered, TotalForksFiltered, ProgrammingLanguage));
				}
				catch (Exception)
				{
					//If problem with TrendEntry exists, just don't add it to the list and stop the program from working.
				}
			}

			return TrendingRepositoriesList;
		}

		public async Task<TrendEntry> GetTrendDetails(TrendEntry entryinformations)
		{
			entryinformations.HasDetails = true;
			HtmlWeb TrendingRepository = new();

			HtmlDocument TrendingRep = TrendingRepository.Load(entryinformations.RespositoryLink);
			HtmlNode ArchiveStatus = TrendingRep.DocumentNode.SelectSingleNode("/html/body/div[1]/div[4]/div/main/div[1]");

			HtmlNode ProjectUrl = TrendingRep.QuerySelector("div.my-3:nth-child(3) > span:nth-child(2) > a:nth-child(1)");
			IList<HtmlNode> Topics = TrendingRep.QuerySelectorAll("a.topic-tag");

			if (ArchiveStatus?.InnerHtml.Contains("This repository has been archived by the owner on", StringComparison.InvariantCultureIgnoreCase) == true)
			{
				entryinformations.IsArchived = true;
			}

			if (ProjectUrl != null)
			{
				entryinformations.HasProjectUrl = true;
				entryinformations.ProjectUrl = ProjectUrl.Attributes["href"].Value;
			}

			if (Topics.Count > 0)
			{
				List<string> TopicNames = new();
				foreach (HtmlNode TopicItem in Topics)
				{
					TopicNames.Add(FilterLineBreaks(TopicItem.InnerHtml));
				}
				entryinformations.Topics = TopicNames.ToArray();
				entryinformations.HasTopics = true;
			}
			else
			{
				entryinformations.Topics = Array.Empty<string>();
			}

			entryinformations.LastCommitTime = $"Last commit: {GetLastCommitTime(entryinformations)}";
			return entryinformations;
		}

		private string GetLastCommitTime(TrendEntry entry)
		{
			string LastCommitTime = "Error";
			string CommitTime = FetchLastCommitTime(entry, "master");

			if (string.IsNullOrEmpty(CommitTime))
			{
				CommitTime = FetchLastCommitTime(entry, "main");
				if (!string.IsNullOrEmpty(CommitTime))
				{
					entry.LastCommitUrl = entry.GetLastCommitUrl("main");
					LastCommitTime = CommitTime;
				}
			}
			else
			{
				entry.LastCommitUrl = entry.GetLastCommitUrl("master");
				LastCommitTime = CommitTime;
			}

			return LastCommitTime;
		}

		private string FetchLastCommitTime(TrendEntry entry, string branch)
		{
			HtmlWeb CommitUrl = new();
			HtmlDocument TrendingRep = CommitUrl.Load(entry.GetLastCommitUrl(branch));

			HtmlNode LastCommitTimeRaw = TrendingRep.QuerySelector("relative-time.no-wrap");

			if (LastCommitTimeRaw?.InnerHtml.Length > 0)
			{
				return FilterLineBreaks(LastCommitTimeRaw.InnerHtml);
			}
			return null;
		}


		/// <summary>
		/// Simple method to remove linebreaks and useless whitespaces that are typically inside the html-strings.
		/// </summary>
		/// <param name="textin">Unfiltered string</param>
		/// <returns>Filtered string</returns>
		private string FilterLineBreaks(string textin)
		{
			StringBuilder sb = new(textin.Length);
			foreach (char i in textin)
			{
				if (i != '\n' && i != '\r' && i != '\t')
				{
					sb.Append(i);
				}
			}
			return sb.ToString().Trim();
		}

		public string FilterGithubUrlCharacters(string textin)
		{
			if (textin != "None" && !string.IsNullOrEmpty(textin))
			{
				string LanguageName = NormalizeLanguageNameIdentifier(textin);
				return $"https://github.com/topics/{LanguageName}";
			}
			else
			{
				return string.Empty;
			}
		}

		private string NormalizeLanguageNameIdentifier(string textin)
		{
			string normalized = textin.Replace(" ", "-").ToLowerInvariant();
			return normalized switch
			{
				"c#" => "csharp",
				"c++" => "cpp",
				_ => normalized,
			};
		}
	}
}
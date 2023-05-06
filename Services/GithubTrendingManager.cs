using System;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using GithubExplorer.Models;

namespace GithubExplorer.Services
{
	public class GithubTrendingManager
	{
		public List<TrendEntry> GetAllTrendEntries(string customquery = "https://github.com/trending")
		{
			List<TrendEntry> entries = new();
			string QueryUrl = customquery;

			HtmlWeb Github = new();
			HtmlDocument GithubDoc = Github.Load(QueryUrl);

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

					string ProgrammingLanguage = "No programming language";
					if (productElement.QuerySelectorAll("article.Box-row > div > span > span") != null)
					{
						IList<HtmlNode> ProgrammingLanguageElementCount = productElement.QuerySelectorAll("article.Box-row > div > span > span");
						if (ProgrammingLanguageElementCount.Count > 0)
						{
							ProgrammingLanguage = ProgrammingLanguageElementCount[1].InnerText;
						}
					}

					entries.Add(new(UsernameFiltered, RepositoryLinkFiltered, RepositoryName, DescriptionFiltered, TotalStarsFiltered, TotalForksFiltered, ProgrammingLanguage));
				}
				catch (Exception)
				{
					//If problem with TrendEntry exists, just don't add it to the list and stop the program from working.
				}
			}

			return entries;
		}

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
	}
}

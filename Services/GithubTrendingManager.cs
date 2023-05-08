using System;
using System.Net;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using GithubExplorer.Models;

namespace GithubExplorer.Services
{
	public class GithubTrendingManager
	{
		public Tuple<List<TrendEntry>, string> GetAllTrendEntries(string customquery = "https://github.com/trending")
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

			string TopLanguage = MostOccuringString(TrendingRepositoriesList);
			return new Tuple<List<TrendEntry>, string>(TrendingRepositoriesList, TopLanguage);
		}

		private string MostOccuringString(List<TrendEntry> trendEntries)
		{
			IEnumerable<IGrouping<string, TrendEntry>> nameGroup = trendEntries.GroupBy(x => x.Programminglanguage);
			int maxCount = nameGroup.Max(g => g.Count());

			string[] mostCommons = nameGroup.Where(x => x.Count() == maxCount).Select(x => x.Key).ToArray();
			return mostCommons[0];
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
	}
}

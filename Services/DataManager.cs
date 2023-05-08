using System;
using System.Linq;
using GithubExplorer.Models;

namespace GithubExplorer.Services
{
	public class DataManager
	{
		public GithubExplorerSettings ActiveSettings { get; set; } = new("https://github.com/trending", "daily", false);

		public string MostUsedProgramminglanguage { get; set; }

		/// <summary>
		/// Parse characters like # and + to still be able to use them in the url, more to come if needed
		/// </summary>
		/// <param name="textin"></param>
		/// <returns></returns>
		public string GithubTopicsProgrammingLanguageUrl(string textin)
		{
			if (textin != "None" && !string.IsNullOrEmpty(textin))
			{
				string LanguageName = textin.Replace(" ", "-");
				if (string.Equals(LanguageName, "C#", StringComparison.InvariantCultureIgnoreCase))
				{
					LanguageName = "csharp";
				}
				else if (string.Equals(LanguageName, "C++", StringComparison.InvariantCultureIgnoreCase))
				{
					LanguageName = "Cpp";
				}
				return $"https://github.com/topics/{LanguageName}";
			}
			else
			{
				return string.Empty;
			}
		}

	}
}


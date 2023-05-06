using System;
using System.Linq;

namespace GithubExplorer.Services
{
	public class DataMan
	{
		public string ActiveGithubUrl { get; set; } = "https://github.com/trending";

		public string MostUsedProgramminglanguage { get; set; }

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

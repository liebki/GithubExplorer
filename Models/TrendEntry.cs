using System;
using System.Linq;

namespace GithubExplorer.Models
{
	public class TrendEntry
	{
		public TrendEntry(string user, string respositoryLink, string respositoryName, string description, string totalStars, string totalForks, string programminglanguage)
		{
			User = user;
			RespositoryLink = respositoryLink;
			RespositoryName = respositoryName;
			Description = description;
			TotalStars = totalStars;
			TotalForks = totalForks;
			Programminglanguage = programminglanguage;
		}

		public string User { get; set; }

		public string RespositoryLink { get; set; }

		public string RespositoryName { get; set; }

		public string Description { get; set; }

		public string TotalStars { get; set; }

		public string TotalForks { get; set; }

		public string Programminglanguage { get; set; }

		public string CreatedByText()
		{
			return $"{this.RespositoryName} created by {this.User}";
		}

	}
}
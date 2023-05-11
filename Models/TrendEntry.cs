using System;
using System.Linq;

namespace GithubExplorer.Models
{
    public class TrendEntry
    {
        public TrendEntry()
        {
        }

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

        public bool HasDetails { get; set; }

        public bool IsArchived { get; set; }

        public bool HasProjectUrl { get; set; }

        public string ProjectUrl { get; set; }

        public bool HasTopics { get; set; }

        public string[] Topics { get; set; }

        public string LastCommitTime { get; set; }

        public string LastCommitUrl { get; set; }


        public string GetLastCommitUrl(string branch)
        {
            return $"https://github.com/{this.User}/{this.RespositoryName}/commit/{branch}";
        }

        public string ParseCreatedByText()
        {
            return $"{this.RespositoryName} created by {this.User}";
        }

        public string GetReadMeUrl(string branch)
        {
            return $"https://raw.githubusercontent.com/{this.User}/{this.RespositoryName}/{branch}/README.md";
        }

        public string GetForksUrl()
        {
            return $"https://github.com/{this.User}/{this.RespositoryName}/forks";
        }

        public string GetStarsUrl()
        {
            return $"https://github.com/{this.User}/{this.RespositoryName}/stargazers";
        }

    }
}
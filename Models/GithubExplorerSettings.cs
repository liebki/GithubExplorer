namespace GithubExplorer.Models;

public class GithubExplorerSettings
{
    public GithubExplorerSettings(string activeGithubUrl, string activeGithubUrlTrendType, bool isDarkmodeEnabled)
    {
        ActiveGithubUrl = activeGithubUrl;
        ActiveGithubUrlTrendType = activeGithubUrlTrendType;
        IsDarkmodeEnabled = isDarkmodeEnabled;
    }

    public string ActiveGithubUrl { get; set; }

    public string ActiveGithubUrlTrendType { get; set; }

    public bool IsDarkmodeEnabled { get; set; }
}
using GithubExplorer.Models;
using Newtonsoft.Json;
using static System.IO.File;

namespace GithubExplorer.Services;

public class ConfigManager
{
    private const string SettingsFileName = "GithubExplorerSettings.json";

    /// <summary>
    ///     Check if the Settings file exists, if not create it with basic values
    /// </summary>
    public GithubExplorerSettings CheckSettings()
    {
        string settingsFilePath = GetAppSettingsFilePath();
        if (!Exists(settingsFilePath)) WriteDefaultSettings();
        return ReadSettings();
    }

    /// <summary>
    ///     Save default settings to the settings file
    /// </summary>
    private static void WriteDefaultSettings()
    {
        GithubExplorerSettings defaultSettings =
            new GithubExplorerSettings("https://github.com/trending?language=csharp", "monthly", false);
        WriteSettings(defaultSettings);
    }

    /// <summary>
    ///     Save the values of the settings object to the JSON file
    /// </summary>
    public static void WriteSettings(GithubExplorerSettings settings)
    {
        string settingsJson = JsonConvert.SerializeObject(settings, Formatting.Indented);
        if (!string.IsNullOrEmpty(settingsJson))
        {
            string settingsFilePath = GetAppSettingsFilePath();
            try
            {
                WriteAllText(settingsFilePath, settingsJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing settings file: " + ex.Message);
                throw;
            }
        }
    }

    /// <summary>
    ///     Read the settings from the JSON file
    /// </summary>
    private GithubExplorerSettings ReadSettings()
    {
        string settingsFilePath = GetAppSettingsFilePath();
        if (!Exists(settingsFilePath)) 
            return null;
        
        try
        {
            string settingsJson = ReadAllText(settingsFilePath);
            if (!string.IsNullOrEmpty(settingsJson))
                return JsonConvert.DeserializeObject<GithubExplorerSettings>(settingsJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading settings file: " + ex.Message);
        }

        return null;
    }

    /// <summary>
    ///     Get the full file path for the settings file
    /// </summary>
    private static string GetAppSettingsFilePath()
    {
        string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        return Path.Combine(documentsFolder, SettingsFileName);
    }
}
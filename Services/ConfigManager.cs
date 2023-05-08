using System;
using System.Linq;
using Newtonsoft.Json;
using GithubExplorer.Models;

namespace GithubExplorer.Services
{
	public class ConfigManager
	{
		private const string SettingsFileName = "GithubExplorerSettings.json";

		/// <summary>
		/// Check if the Settingsfile exists, if not create it with basic values
		/// </summary>
		public GithubExplorerSettings CheckSettings()
		{
			string SettingsFilePath = GetAppSettingsFolder();
			if (!File.Exists(SettingsFilePath))
			{
				WriteSettings(new("https://github.com/trending", "daily", false));
			}
			return ReadSettings();
		}

		/// <summary>
		/// Save the values of the values from this class and put it inside of a .json file to use later
		/// </summary>
		public void WriteSettings(GithubExplorerSettings settings)
		{
			string SettingsJson = JsonConvert.SerializeObject(settings);
			if (!string.IsNullOrEmpty(SettingsJson))
			{
				File.WriteAllText(GetAppSettingsFolder(), SettingsJson);
			}
		}

		/// <summary>
		/// Read the .json and use the values in this class to make it available in the whole application
		/// </summary>
		public GithubExplorerSettings ReadSettings()
		{
			string SettingsJson;
			try
			{
				SettingsJson = File.ReadAllText(GetAppSettingsFolder());
			}
			catch (Exception)
			{
				return null;
			}

			if (!string.IsNullOrEmpty(SettingsJson))
			{
				GithubExplorerSettings Settings = JsonConvert.DeserializeObject<GithubExplorerSettings>(SettingsJson);
				if (SettingsJson != null)
				{
					return Settings;
				}
			}
			return null;
		}

		/// <summary>
		/// Get the folder where the settings file should be saved, theoretically it's identical from Win2Mac it's the execution directory
		/// </summary>
		/// <returns>Directory as string</returns>
		private string GetAppSettingsFolder()
		{
			if (OperatingSystem.IsWindows())
			{
				return Path.Combine(AppContext.BaseDirectory, SettingsFileName);
			}
			else if (OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst())
			{
				return Path.Combine(Path.GetFullPath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName), SettingsFileName);
			}
			else
			{
				return Path.Combine(FileSystem.Current.AppDataDirectory, SettingsFileName);
			}

		}

	}
}

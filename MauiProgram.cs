using GithubExplorer.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using NavigationManagerUtils;

namespace GithubExplorer;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<GithubTrendingManager>();
        builder.Services.AddTransient<NavManUtils>();

        builder.Services.AddSingleton<DataMan>();
        builder.Services.AddMudServices();

        return builder.Build();
	}
}

using Microsoft.AspNetCore.Components.WebView;

namespace GithubExplorer;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void BlazorWebViewInitialized(object sender, BlazorWebViewInitializedEventArgs e)
	{
#if WINDOWS
		e.WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
		e.WebView.CoreWebView2.Settings.IsGeneralAutofillEnabled = false;
		e.WebView.CoreWebView2.Settings.IsPasswordAutosaveEnabled = false;
		e.WebView.CoreWebView2.Settings.IsPinchZoomEnabled = false;
#endif
	}
}

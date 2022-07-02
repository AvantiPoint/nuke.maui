using MauiCIDemo.Views;

namespace MauiCIDemo;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
            .AddGlobalNavigationObserver((c,o) =>
                o.Subscribe(context =>
                {
                    if (!context.Result.Success && !context.Cancelled)
                        Console.WriteLine(context.Result.Exception);
                }))
                .OnAppStart("NavigationPage/MainPage");
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainPage>()
                     .RegisterInstance(SemanticScreenReader.Default);
    }
}

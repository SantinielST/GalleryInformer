using GalleryInformer.Services;
using GalleryInformer.ViewModels;
using GalleryInformer.Views;
using Microsoft.Extensions.Logging;

namespace GalleryInformer;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<NavigationService>();

        builder.Services.AddSingleton<PinPage>();
        builder.Services.AddSingleton<PinPageViewModel>();

        builder.Services.AddSingleton<GalleryPage>();
        builder.Services.AddSingleton<GalleryPageViewModel>();

        builder.Services.AddSingleton<ImageDetailPage>();
        builder.Services.AddSingleton<ImageDetailViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

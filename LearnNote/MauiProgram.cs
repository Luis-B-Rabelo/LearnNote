using LearnNote.Source.MVVM.ViewModels;
using LearnNote.Source.MVVM.Views;
using Microsoft.Extensions.Logging;

namespace LearnNote
{
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

            builder.Services.AddTransient<StartPage>();
            builder.Services.AddTransient<StartViewModel>();

            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<HomeViewModel>();

            builder.Services.AddTransient<SignInPage>();
            builder.Services.AddTransient<SignInViewModel>();

            builder.Services.AddTransient<SignUpPage>();
            builder.Services.AddTransient<SignUpViewModel>();


            #if DEBUG
                builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}

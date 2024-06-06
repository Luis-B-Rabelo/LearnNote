using CommunityToolkit.Maui;
using LearnNote.Source.MVVM.ViewModels;
using LearnNote.Source.MVVM.ViewModels.PopUps;
using LearnNote.Source.MVVM.Views;
using LearnNote.Source.MVVM.Views.PopUps;
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
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("Forum-Regular.ttf", "ForumRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddTransient<StartPage>();
            builder.Services.AddTransient<StartViewModel>();

            builder.Services.AddTransient<SignInPage>();
            builder.Services.AddTransient<SignInViewModel>();

            builder.Services.AddTransient<SignUpPage>();
            builder.Services.AddTransient<SignUpViewModel>();

            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<HomeViewModel>();

            builder.Services.AddSingleton<MyNotebooksPage>();
            builder.Services.AddSingleton<MyNotebooksViewModel>();

            builder.Services.AddTransient<NotebookPage>();
            builder.Services.AddTransient<NotebookViewModel>();

            builder.Services.AddTransient<NotePage>();
            builder.Services.AddTransient<NoteViewModel>();

            builder.Services.AddTransient<PlannerPage>();
            builder.Services.AddTransient<PlannerViewModel>();

            builder.Services.AddSingleton<CalendarPage>();
            builder.Services.AddSingleton<CalendarViewModel>();

            builder.Services.AddTransient<ActivityPage>();
            builder.Services.AddTransient<ActivityViewModel>();

            builder.Services.AddTransient<AlarmPage>();
            builder.Services.AddTransient<AlarmViewModel>();

            builder.Services.AddSingleton<ConfigsPage>();
            builder.Services.AddSingleton<ConfigsViewModel>();

            builder.Services.AddTransientPopup<LoginErrorPopUp, LoginErrorViewModel>();

            builder.Services.AddTransientPopup<AddNotebookPopUp, AddNotebookViewModel>();

            builder.Services.AddTransientPopup<AddNotePopUp, AddNoteViewModel>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

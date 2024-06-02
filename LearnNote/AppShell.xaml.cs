using LearnNote.Source.MVVM.Views;

namespace LearnNote
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));

            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));

            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));

            Routing.RegisterRoute(nameof(MyNotebooksPage), typeof(MyNotebooksPage));

            Routing.RegisterRoute(nameof(NotebookPage), typeof(NotebookPage));

            Routing.RegisterRoute(nameof(NotePage), typeof(NotePage));

            Routing.RegisterRoute(nameof(PlannerPage), typeof(PlannerPage));

            Routing.RegisterRoute(nameof(CalendarPage), typeof(CalendarPage));

            Routing.RegisterRoute(nameof(ActivityPage), typeof(ActivityPage));

            Routing.RegisterRoute(nameof(AlarmPage), typeof(AlarmPage));

            Routing.RegisterRoute(nameof(ConfigsPage), typeof(ConfigsPage));

        }
    }
}

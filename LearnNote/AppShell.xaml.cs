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

        }
    }
}

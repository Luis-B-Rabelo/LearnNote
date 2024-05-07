using LearnNote.Source.MVVM.ViewModels;

namespace LearnNote.Source.MVVM.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage(HomeViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}

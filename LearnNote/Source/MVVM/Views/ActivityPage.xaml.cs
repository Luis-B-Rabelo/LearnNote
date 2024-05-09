using LearnNote.Source.MVVM.ViewModels;

namespace LearnNote.Source.MVVM.Views;

public partial class ActivityPage : ContentPage
{
	public ActivityPage(ActivityViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
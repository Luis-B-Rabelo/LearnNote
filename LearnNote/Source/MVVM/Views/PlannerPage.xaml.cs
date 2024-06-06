using LearnNote.Source.MVVM.ViewModels;

namespace LearnNote.Source.MVVM.Views;

public partial class PlannerPage : ContentPage
{
	public PlannerPage(PlannerViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

    }
}

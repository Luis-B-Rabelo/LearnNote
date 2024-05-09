using LearnNote.Source.MVVM.ViewModels;

namespace LearnNote.Source.MVVM.Views;

public partial class AlarmPage : ContentPage
{
	public AlarmPage(AlarmViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
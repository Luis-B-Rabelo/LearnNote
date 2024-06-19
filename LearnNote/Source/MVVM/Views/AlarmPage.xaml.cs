using LearnNote.Source.MVVM.ViewModels;

namespace LearnNote.Source.MVVM.Views;

public partial class AlarmPage : ContentPage
{
	public AlarmPage(AlarmViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    private void StartButton_Clicked(object sender, EventArgs e)
    {
        (BindingContext as AlarmViewModel)?.StartTimer();
    }

    private void PauseButton_Clicked(object sender, EventArgs e)
    {
        (BindingContext as AlarmViewModel)?.PauseTimer();
    }

    private void ResetButton_Clicked(object sender, EventArgs e)
    {
        (BindingContext as AlarmViewModel)?.ResetTimer();
    }
}
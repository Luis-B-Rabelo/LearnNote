using LearnNote.Source.MVVM.ViewModels;

namespace LearnNote.Source.MVVM.Views;

public partial class ConfigsPage : ContentPage
{
	public ConfigsPage(ConfigsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
using LearnNote.Source.MVVM.ViewModels;

namespace LearnNote.Source.MVVM.Views;

public partial class NotebookPage : ContentPage
{
	public NotebookPage(NotebookViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
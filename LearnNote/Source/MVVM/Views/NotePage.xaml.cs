using LearnNote.Source.MVVM.ViewModels;

namespace LearnNote.Source.MVVM.Views;

public partial class NotePage : ContentPage
{
	public NotePage(NoteViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
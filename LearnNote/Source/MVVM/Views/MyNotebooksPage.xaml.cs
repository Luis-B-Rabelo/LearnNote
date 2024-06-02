using LearnNote.Source.MVVM.ViewModels;

namespace LearnNote.Source.MVVM.Views;

public partial class MyNotebooksPage : ContentPage
{
	public MyNotebooksPage(MyNotebooksViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }
}
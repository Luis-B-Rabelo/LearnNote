using LearnNote.Source.MVVM.ViewModels;
using Microsoft.Maui.Controls;

namespace LearnNote.Source.MVVM.Views;

public partial class NotePage : ContentPage
{
	public NotePage(NoteViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    private void NoteFontSizeChanged(object sender, TextChangedEventArgs e)
    {
        NoteText.FontSize = double.Parse(NoteFontSize.Text);
    }
}
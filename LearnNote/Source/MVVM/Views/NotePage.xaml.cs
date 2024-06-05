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
        if (double.TryParse(NoteFontSize.Text, out double fontSize)) //troquei double.Parse por TryParse, pois ele não dá erro caso o valor não seja um número
        {
            if (NoteText != null)
            {
                NoteText.FontSize = fontSize;
            }
        }
    }
}

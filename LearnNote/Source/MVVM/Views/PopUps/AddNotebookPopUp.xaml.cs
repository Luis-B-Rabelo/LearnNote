using CommunityToolkit.Maui.Views;
using LearnNote.Source.MVVM.ViewModels.PopUps;

namespace LearnNote.Source.MVVM.Views.PopUps;

public partial class AddNotebookPopUp : Popup
{
	public AddNotebookPopUp(AddNotebookViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private void CanClose(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
		Close();
    }
}
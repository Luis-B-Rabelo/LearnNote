using CommunityToolkit.Maui.Views;
using LearnNote.Source.MVVM.ViewModels.PopUps;

namespace LearnNote.Source.MVVM.Views.PopUps;

public partial class DeleteNotebookPopUp : Popup
{
	public DeleteNotebookPopUp(DeleteNotebookViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
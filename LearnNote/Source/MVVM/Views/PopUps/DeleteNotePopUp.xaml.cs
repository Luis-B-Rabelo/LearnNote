using CommunityToolkit.Maui.Views;
using LearnNote.Source.MVVM.ViewModels.PopUps;

namespace LearnNote.Source.MVVM.Views.PopUps;

public partial class DeleteNotePopUp : Popup
{
	public DeleteNotePopUp(DeleteNoteViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
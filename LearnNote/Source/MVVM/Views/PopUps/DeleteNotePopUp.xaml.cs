using CommunityToolkit.Maui.Views;
using LearnNote.Source.MVVM.ViewModels.PopUps;

namespace LearnNote.Source.MVVM.Views.PopUps;

public partial class DeleteNotePopUp : Popup
{
	public DeleteNotePopUp(uint noteId, string noteTitle, uint notebookId, uint userId)
	{
		InitializeComponent();
		BindingContext = new DeleteNoteViewModel(noteId, noteTitle, notebookId, userId);
    }
}
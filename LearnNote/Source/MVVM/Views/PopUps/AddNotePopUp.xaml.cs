using CommunityToolkit.Maui.Views;
using LearnNote.Source.MVVM.ViewModels.PopUps;

namespace LearnNote.Source.MVVM.Views.PopUps;

public partial class AddNotePopUp : Popup
{
	public AddNotePopUp(AddNoteViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    void OnAddNoteTapped(object? sender, EventArgs e) => Close();
}
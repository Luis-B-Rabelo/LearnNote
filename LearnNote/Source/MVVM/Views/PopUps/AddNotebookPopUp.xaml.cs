using CommunityToolkit.Maui.Views;
using LearnNote.Source.MVVM.ViewModels.PopUps;
using System.Runtime.CompilerServices;

namespace LearnNote.Source.MVVM.Views.PopUps;

public partial class AddNotebookPopUp : Popup
{


	public AddNotebookPopUp(AddNotebookViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
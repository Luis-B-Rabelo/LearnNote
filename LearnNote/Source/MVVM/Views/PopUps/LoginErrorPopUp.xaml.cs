using CommunityToolkit.Maui.Views;
using LearnNote.Source.MVVM.ViewModels.PopUps;

namespace LearnNote.Source.MVVM.Views.PopUps;

public partial class LoginErrorPopUp : Popup
{
	public LoginErrorPopUp(LoginErrorViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private void OnOkButtonClicked(object sender, EventArgs e)
    {
		Close();
    }
}
using CommunityToolkit.Maui.Views;
using LearnNote.Model;
using LearnNote.Source.MVVM.ViewModels.PopUps;

namespace LearnNote.Source.MVVM.Views.PopUps;

public partial class ManageActivityPopUp : Popup
{
	public ManageActivityPopUp(ActivityModel activity, uint userId)
	{
		InitializeComponent();
		BindingContext = new ManageActivityViewModel(activity, userId);
	}

    private void OnCloseButtonClick(object sender, EventArgs e)
    {
		Close();
    }
}
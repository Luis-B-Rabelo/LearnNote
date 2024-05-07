using LearnNote.Source.MVVM.ViewModels;

namespace LearnNote.Source.MVVM.Views;

public partial class SignInPage : ContentPage
{
    public SignInPage(SignInViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

    }

    


}
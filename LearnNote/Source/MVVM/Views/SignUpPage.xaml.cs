using LearnNote.Source.MVVM.ViewModels;

namespace LearnNote.Source.MVVM.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    
}
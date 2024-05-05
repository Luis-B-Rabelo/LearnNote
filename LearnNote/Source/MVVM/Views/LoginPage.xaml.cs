namespace LearnNote.Source.MVVM.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void LoginBtn(object sender, EventArgs e)
	{
		await DisplayAlert("Login", "Login bem-sucedido", "OK");
		await Navigation.PushAsync(new MainPage());
		
	}

	private void OnEntryEmailChanged(object sender, TextChangedEventArgs e)
	{
        string oldText = e.OldTextValue;
		string newText = e.NewTextValue;
		string myText = email.Text;
	}

	private void OnEntryEmailCompleted(object sender, EventArgs e)
	{
		string text = ((Entry)sender).Text;
	}

    private void OnEntryPasswordChanged(object sender, TextChangedEventArgs e)
    {
        string oldText = e.OldTextValue;
        string newText = e.NewTextValue;
        string myText = password.Text;
    }

    private void OnEntryPasswordCompleted(object sender, EventArgs e)
    {
        string text = ((Entry)sender).Text;
    }

	private async void NavigateToRegisterPage(object sender, TappedEventArgs e)
	{
		await DisplayAlert("Cadastro", "Você foi redirecionado para a página de cadastro", "OK");
		await Navigation.PushAsync(new RegistrationPage());
	}
}
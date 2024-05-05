namespace LearnNote.Source.MVVM.Views;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
	}

    private async void RegisterBtn(object sender, EventArgs e)
    {
        await DisplayAlert("Cadastro", "Cadastro bem-sucedido", "OK");
        await Navigation.PopAsync();

    }

    // Revisar métodos abaixo e organizá-los
    // Revisar Design também - Ao clicar fora de um Entry, ele é direcionado para o Entry do Username
    private void OnEntryUsernameChanged(object sender, TextChangedEventArgs e)
    {
        string oldText = e.OldTextValue;
        string newText = e.NewTextValue;
        string myText = username.Text;
    }

    private void OnEntryUsenameCompleted(object sender, EventArgs e)
    {
        string text = ((Entry)sender).Text;
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

    private void OnEntryConfirmPasswordChanged(object sender, TextChangedEventArgs e)
    {
        string oldText = e.OldTextValue;
        string newText = e.NewTextValue;
        string myText = confirmPassword.Text;
    }

    private void OnEntryConfirmPasswordCompleted(object sender, EventArgs e)
    {
        string text = ((Entry)sender).Text;
    }


    private async void ReturnToLoginPage(object sender, EventArgs e)
    {
        await DisplayAlert("Retorno", "Você retornou ao Login Page", "OK");
        await Navigation.PopAsync();
    }
}
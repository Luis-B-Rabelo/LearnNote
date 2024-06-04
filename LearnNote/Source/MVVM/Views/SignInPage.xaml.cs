using LearnNote.Source.MVVM.ViewModels;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LearnNote.Source.MVVM.Views;

public partial class SignInPage : ContentPage
{
    private bool[] _validation;

    public SignInPage(SignInViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

        _validation = new bool[2];
        SignInButton.IsEnabled = false;
    }

    private void EmailEntryChanged(object sender, TextChangedEventArgs e)
    {
        if (IsValidEmail(EmailEntry.Text))
        {
            _validation[0] = true;
            EmailLabel.TextDecorations = TextDecorations.None;
        }
        else
        {
            _validation[0] = false;
            EmailLabel.TextDecorations = TextDecorations.Strikethrough;
        }

        if (_validation[0] && _validation[1]) 
        {
            SignInButton.IsEnabled = true;
        }
    }

    private void PasswordEntryChanged(object sender, TextChangedEventArgs e)
    {
        if(IsValidPassword(PasswordEntry.Text))
        {
            _validation[1] = true;
            PasswordLabel.TextDecorations = TextDecorations.None;
        }
        else 
        {
            _validation[1] = false;
            PasswordLabel.TextDecorations = TextDecorations.Strikethrough;
        }

        if (_validation[0] && _validation[1])
        {
            SignInButton.IsEnabled = true;
        }
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        try
        {
            Regex regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            bool isValidated = regex.IsMatch(password);

            return isValidated;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}
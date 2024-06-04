using LearnNote.Source.MVVM.ViewModels;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LearnNote.Source.MVVM.Views;

public partial class SignUpPage : ContentPage
{
    private bool[] _validation;

    public SignUpPage(SignUpViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

        _validation = new bool[4];
        SignUpButton.IsEnabled = false;
    }

    private void NameEntryChanged(object sender, TextChangedEventArgs e)
    {
        if (IsValidName(NameEntry.Text))
        {
            _validation[0] = true;
            NameLabel.TextDecorations = TextDecorations.None;
        }
        else
        {
            _validation[0] = false;
            NameLabel.TextDecorations = TextDecorations.Strikethrough;
        }

        if (_validation[0] && _validation[1] && _validation[2] && _validation[3])
        {
            SignUpButton.IsEnabled = true;
        }
    }

    private void EmailEntryChanged(object sender, TextChangedEventArgs e)
    {
        if (IsValidEmail(EmailEntry.Text))
        {
            _validation[1] = true;
            EmailLabel.TextDecorations = TextDecorations.None;
        }
        else
        {
            _validation[1] = false;
            EmailLabel.TextDecorations = TextDecorations.Strikethrough;
        }

        if (_validation[0] && _validation[1] && _validation[2] && _validation[3])
        {
            SignUpButton.IsEnabled = true;
        }
    }

    private void PasswordEntryChanged(object sender, TextChangedEventArgs e)
    {
        if (IsValidPassword(PasswordEntry.Text))
        {
            _validation[2] = true;
            PasswordLabel.TextDecorations = TextDecorations.None;
        }
        else
        {
            _validation[2] = false;
            PasswordLabel.TextDecorations = TextDecorations.Strikethrough;
        }

        if (_validation[0] && _validation[1] && _validation[2] && _validation[3])
        {
            SignUpButton.IsEnabled = true;
        }
    }



    private void PasswordConfirmEntryChanged(object sender, TextChangedEventArgs e)
    {
        if (ConfirmPassword(PasswordEntry.Text, PasswordConfirmEntry.Text))
        {
            _validation[3] = true;
            PasswordConfirmLabel.TextDecorations = TextDecorations.None;
        }
        else
        {
            _validation[3] = false;
            PasswordConfirmLabel.TextDecorations = TextDecorations.Strikethrough;
        }

        if (_validation[0] && _validation[1] && _validation[2] && _validation[3])
        {
            SignUpButton.IsEnabled = true;
        }
    }

    public static bool IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        try
        {
            Regex regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z]).{8,}$");

            bool isValidated = regex.IsMatch(name);

            return isValidated;
        }
        catch (Exception ex)
        {
            return false;
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
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ConfirmPassword(string password1, string password2)
    {
        if (string.IsNullOrWhiteSpace(password2))
            return false;

        try
        {
            if(password1 == password2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }



}
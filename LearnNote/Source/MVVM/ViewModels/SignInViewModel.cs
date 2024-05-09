using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.Views;

namespace LearnNote.Source.MVVM.ViewModels
{
    public partial class SignInViewModel : ObservableObject
    {
        #region Properties

        private string? _email;

        private string? _password;
        #endregion

        #region Getters & Setters

        public string? Email
        {
            get => _email;

            set
            {
                _email = value;
                OnPropertyChanged("SignInEmail");
            }
        }

        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("SignInPassword");
            }
        } 
        #endregion

        public SignInViewModel()
        {
            #if DEBUG
                GlobalFunctionalities.Logger.Debug("Carregando página de Login");
            #endif
        }

        [RelayCommand]
        async Task SignIn()
        {
            try
            {
                UserDAO userDAO = new UserDAO();
                if (userDAO.ConfirmUser(Email, Password))
                {
                    await Shell.Current.GoToAsync($"{nameof(HomePage)}?Email={Email}");
                }
                else
                {
                    await Shell.Current.GoToAsync(nameof(SignInPage));
                }

#if DEBUG
                GlobalFunctionalities.Logger.Debug("Logando usuário");
                #endif
            }
            catch (Exception ex)
            {
                GlobalFunctionalities.Logger.Error(ex);
            }
        }

        [RelayCommand]
        async Task NavigateToSignUp()
        {
            try 
            { 
                await Shell.Current.GoToAsync(nameof(SignUpPage));

                #if DEBUG
                    GlobalFunctionalities.Logger.Debug("Navegando para a Pagina de Cadastro");
                #endif
            }
            catch (Exception ex) 
            { 
                GlobalFunctionalities.Logger.Error(ex);
            }
        }
    }


}

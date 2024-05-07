using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Source.MVVM.Views;

namespace LearnNote.Source.MVVM.ViewModels
{
    public partial class SignInViewModel : ObservableObject
    {
        #region Properties
        private string _pageName;

        private string? _email;

        private string? _password;
        #endregion

        #region Getters & Setters
        public string PageName
        {
            get => _pageName;
        }

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
            _pageName = "SignInPage";

        }

        [RelayCommand]
        async Task SignIn()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(SignUpPage));

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

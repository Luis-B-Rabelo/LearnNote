using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.Views;

namespace LearnNote.Source.MVVM.ViewModels
{
    public partial class SignUpViewModel : ObservableObject
    {
        #region Properties
        private string _pageName;

        private string? _userName;

        private string? _email;

        private string? _password;

        private string? _passwordConfirm; 
        #endregion

        #region Getters & Setters
        public string PageName
        {
            get => _pageName;
        }

        public string? UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged("SignUpUserName");
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged("SignUpEmail");
            }
        }

        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("SignUpUserPassword");
            }
        }

        public string? PasswordConfirm
        {
            get => _passwordConfirm;
            set
            {
                _passwordConfirm = value;
                OnPropertyChanged("SignUpPasswordConfirm");
            }
        } 
        #endregion

        public SignUpViewModel()
        {
            #if DEBUG
                GlobalFunctionalities.Logger.Debug("Carregando página de Cadastro");
            #endif
            _pageName = "SignUpPage";
        }

        [RelayCommand]
        async Task NavigateToSignIn()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(SignInPage));
                #if DEBUG
                    GlobalFunctionalities.Logger.Debug("Navegando para a Pagina de Login");
                #endif
            }
            catch (Exception ex)
            {
                GlobalFunctionalities.Logger.Error(ex);
            }
        }

        [RelayCommand]
        async Task SignUp()
        {
            try
            {
                UserDAO userDAO = new UserDAO();
                if (userDAO.CreateNewUser(Email, UserName, Password))
                {
                    await Shell.Current.GoToAsync(nameof(HomePage));
                }
                else
                {
                    await Shell.Current.GoToAsync(nameof(SignUpPage));
                }

                #if DEBUG
                    GlobalFunctionalities.Logger.Debug("Cadastrando usuário");
                #endif
            }
            catch (Exception ex)
            {
                GlobalFunctionalities.Logger.Error(ex);
            }
        }
    }
}

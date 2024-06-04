using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.ViewModels.PopUps;
using LearnNote.Source.MVVM.Views;

namespace LearnNote.Source.MVVM.ViewModels
{
    public partial class SignUpViewModel : ObservableObject
    {
        #region Properties
        private readonly IPopupService _popupService;

        private string? _userName;

        private string? _email;

        private string? _password;

        private string? _passwordConfirm; 
        #endregion

        #region Getters & Setters

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

        public SignUpViewModel(IPopupService popupService)
        {
            #if DEBUG
                GlobalFunctionalities.Logger.Debug("Carregando página de Cadastro");
#endif

            _popupService = popupService;
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
                switch (UserDAO.CreateNewUser(Email, UserName, Password))
                {
                    case 1:
#if DEBUG
                        GlobalFunctionalities.Logger.Debug("Cadastrando usuário");
#endif
                        await Shell.Current.GoToAsync($"{nameof(HomePage)}?PassEmail={Email}");
                        break;
                    case 2:
                        await Shell.Current.GoToAsync(nameof(SignUpPage));
                        _popupService.ShowPopup<LoginErrorViewModel>(onPresenting: viewModel => viewModel.Error = "Email já cadastrado");
                        break; 
                    case 3:
                        await Shell.Current.GoToAsync(nameof(SignUpPage));
                        _popupService.ShowPopup<LoginErrorViewModel>(onPresenting: viewModel => viewModel.Error = "Erro na hora do cadastro, tente novamente mais tarde");
                        break;
                }


            }
            catch (Exception ex)
            {
                GlobalFunctionalities.Logger.Error(ex);
                await Shell.Current.GoToAsync(nameof(SignUpPage));
            }
        }
    }
}

using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.ViewModels.PopUps;
using LearnNote.Source.MVVM.Views;

namespace LearnNote.Source.MVVM.ViewModels
{
    public partial class SignInViewModel : ObservableObject
    {
        #region Properties

        private readonly IPopupService _popupService;

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

        public SignInViewModel(IPopupService popupService)
        {
#if DEBUG
                GlobalFunctionalities.Logger.Debug("Carregando página de Login");
#endif
            _popupService = popupService;
        }

        [RelayCommand]
        async Task SignIn()
        {
            try
            {
                if(UserDAO.ConfirmUser(Email, Password))
                {
#if DEBUG
                    GlobalFunctionalities.Logger.Debug("Logando usuário");
#endif
                    await Shell.Current.GoToAsync($"{nameof(HomePage)}?PassEmail={Email}");
                }
                else
                {
                    await Shell.Current.GoToAsync(nameof(SignInPage));
                    _popupService.ShowPopup<LoginErrorViewModel>(onPresenting: viewModel => viewModel.Error = "Email ou Senha incorretos");
                }
            }
            catch (Exception ex)
            {
                GlobalFunctionalities.Logger.Error(ex);
                await Shell.Current.GoToAsync(nameof(SignInPage));
                _popupService.ShowPopup<LoginErrorViewModel>(onPresenting: viewModel => viewModel.Error = "Erro no sistema, tente reiniciar o app");
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

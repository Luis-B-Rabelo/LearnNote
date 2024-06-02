using CommunityToolkit.Mvvm.ComponentModel;
using LearnNote.Source.Core;
using LearnNote.Source.MVVM.Views;

namespace LearnNote.Source.MVVM.ViewModels
{
    public partial class StartViewModel : ObservableObject
    {
        public StartViewModel() 
        {
            #if DEBUG
                GlobalFunctionalities.Logger.Debug("Inicializando App");
            #endif
            try
            {
                if (!MySqlConn.TestConnection())
                    throw new Exception();

                if (!(Directory.Exists($@"{AppDomain.CurrentDomain.BaseDirectory}\Storage")))
                {
                    Directory.CreateDirectory($@"{AppDomain.CurrentDomain.BaseDirectory}\Storage\Users");
#if DEBUG
                    GlobalFunctionalities.Logger.Debug("Criando diretório de armazenamento");
#endif
                }

                Shell.Current.GoToAsync(nameof(SignInPage));

                #if DEBUG
                    GlobalFunctionalities.Logger.Debug("Navegando para a Pagina de Login");
                #endif
            }
            catch (Exception ex)
            {
                GlobalFunctionalities.Logger.Error(ex);
            }


        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using LearnNote.Source.DAO;
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
                MySqlConn testConn = new MySqlConn();
                if (!testConn.TestConnection())
                    throw new Exception();
                testConn = null;

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

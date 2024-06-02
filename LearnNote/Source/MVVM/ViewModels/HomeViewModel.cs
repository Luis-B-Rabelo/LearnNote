using LearnNote.Model;
using LearnNote.Source.Core;
using LearnNote.Source.DAO;
using NLog;

namespace LearnNote.Source.MVVM.ViewModels;

public partial class HomeViewModel : NavBar, IQueryAttributable
{
    private UserModel _user;

    public UserModel User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged("User");
        }
    }

    private string _passEmail;

    private string PassEmail
    {
        get => _passEmail;
        set
        {
            _passEmail = value;
            OnPropertyChanged("PassEmail");
        }
    }

    public HomeViewModel() 
    {
        
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if(query.ContainsKey("PassEmail"))
        {
            PassEmail = query["PassEmail"].ToString();
            User = UserDAO.SelectUser(PassEmail);
            UserId = User.UserId;

#if DEBUG
            GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Parametro passado e puxando usuário")
                .Property("PassEmail", PassEmail)
                .Property("User", _user)
                .Log();
#endif

        }
    }

}

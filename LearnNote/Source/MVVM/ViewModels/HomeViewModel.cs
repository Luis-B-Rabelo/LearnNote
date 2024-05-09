using CommunityToolkit.Mvvm.ComponentModel;
using LearnNote.Model;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.Views;

namespace LearnNote.Source.MVVM.ViewModels;

[QueryProperty("Email", "Email")]
public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty]
    private string _email;

    private uint _userId;

    private string _userName;

    public string UserName
    {
        get => _userName;
    }

    public uint UserId
    {
        get => _userId;
    }

    private UserModel _user;

    public UserModel User
    {
        get => _user;
        set
        {
            _user = value;
            _userName = _user.UserName;
            _userId = _user.UserId;
        }
    }

    public HomeViewModel() 
    {
        UserDAO userDAO = new UserDAO();
        if(Email is not null)
            _user = userDAO.SelectUser(Email);
        else
            Shell.Current.GoToAsync(nameof(SignInPage));
    }
}

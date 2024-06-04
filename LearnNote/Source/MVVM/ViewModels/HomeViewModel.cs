using CommunityToolkit.Mvvm.Input;
using LearnNote.Model;
using LearnNote.Source.Core;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.Views;
using NLog;
using System.Collections.ObjectModel;

namespace LearnNote.Source.MVVM.ViewModels;

public partial class HomeViewModel : NavBar, IQueryAttributable
{
    private UserModel _user;

    private string _passEmail;

    private ObservableCollection<NotebookModel> _notebooks;

    private ObservableCollection<NoteModel> _notes;

    public UserModel User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged("User");
        }
    }

    public ObservableCollection<NotebookModel> Notebooks
    {
        get => _notebooks;
        set
        {
            _notebooks = value;
            OnPropertyChanged("Notebooks");
        }
    }

    public ObservableCollection<NoteModel> Notes
    {
        get => _notes;
        set
        {
            _notes = value;
            OnPropertyChanged("Notes");
        }
    }

    public uint NotebookId { get; private set; }

    public uint NoteId { get; private set; }

    public string Title { get; private set; }

    public ushort QuantityNotes { get; private set; }

    public HomeViewModel() 
    {
        
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if(query.ContainsKey("PassEmail"))
        {
            _passEmail = query["PassEmail"].ToString();
            User = UserDAO.SelectUser(_passEmail);
            UserId = User.UserId;

#if DEBUG
            GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Parametro passado e puxando usuário")
                .Property("PassEmail", _passEmail)
                .Property("User", _user)
                .Log();
#endif

        }

        Notebooks = NotebookDAO.Select4UserNotebooks(UserId);
        Notes = NoteDAO.SelectRecentNotebookNotes(UserId);
    }

    [RelayCommand]
    public async Task OpenNotebook(uint notebookId)
    {
        await Shell.Current.GoToAsync($"{nameof(NotebookPage)}?PassNotebookId={notebookId}");
    }

    [RelayCommand]
    public async Task OpenNote(uint noteId)
    {
        await Shell.Current.GoToAsync($"{nameof(NotePage)}?PassNoteId={noteId}&PassUserId={UserId}");
    }
}

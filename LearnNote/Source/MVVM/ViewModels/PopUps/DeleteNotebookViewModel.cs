
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.Views;
using NLog;
using System.ComponentModel;

namespace LearnNote.Source.MVVM.ViewModels.PopUps
{
    public partial class DeleteNotebookViewModel : ObservableObject, INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _notebookTitle;

        private uint _notebookId;

        private uint _userIdFk;

        #endregion

        #region Getters & Setters
        public string NotebookTitle
        {
            get => _notebookTitle;
            set
            {
                _notebookTitle = value;
                OnPropertyChanged();
            }
        }

        public uint NotebookId
        {
            get => _notebookId;
            set
            {
                _notebookId = value;
                OnPropertyChanged();
            }
        }

        public uint UserIdFk
        {
            get => _userIdFk;
            set
            {
                _userIdFk = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public DeleteNotebookViewModel(uint notebookId, string notebookTitle, uint userId) 
        {
            NotebookId = notebookId;
            NotebookTitle = notebookTitle;
            UserIdFk = userId;
        }

        [RelayCommand]
        public void DeleteNotebook(Popup popup)
        {

#if DEBUG
            GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Deletando caderno")
                .Property("NotebookId", _notebookId)
                .Log();
#endif

            if (NotebookDAO.DeleteNotebook(NotebookId, UserIdFk))
            {
                Shell.Current.GoToAsync($"{nameof(MyNotebooksPage)}?PassUserId={UserIdFk}");
                popup.Close();
            }
            else
            {
                Shell.Current.GoToAsync($"{nameof(MyNotebooksPage)}?PassUserId={UserIdFk}");
                popup.Close();
            }
        }
    }
}

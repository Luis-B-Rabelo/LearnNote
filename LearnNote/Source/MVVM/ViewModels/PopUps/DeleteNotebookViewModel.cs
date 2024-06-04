
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

        private string _title;

        private uint _notebookId;

        private uint _userIdFk;

        #endregion

        #region Getters & Setters
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
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

        [RelayCommand]
        public async Task DeleteNotebook()
        {

#if DEBUG
            GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Deletando caderno")
                .Property("NotebookId", _notebookId)
                .Log();
#endif

            if (NotebookDAO.DeleteNotebook(NotebookId, UserIdFk))
            {
                Thread.Sleep(500);
                await Shell.Current.GoToAsync($"{nameof(MyNotebooksPage)}?PassUserId={UserIdFk}");
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(MyNotebooksPage)}?PassUserId={UserIdFk}");
            }
        }
    }
}

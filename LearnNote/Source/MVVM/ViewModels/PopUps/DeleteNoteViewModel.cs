using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.Views;
using NLog;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace LearnNote.Source.MVVM.ViewModels.PopUps
{
    public partial class DeleteNoteViewModel : ObservableObject, INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _noteTitle;

        private uint _noteId;

        private uint _notebookId;

        private uint _userIdFk;

        #endregion

        #region Getters & Setters
        public string NoteTitle
        {
            get => _noteTitle;
            set
            {
                _noteTitle = value;
                OnPropertyChanged();
            }
        }

        public uint NoteId
        {
            get => _noteId;
            set
            {
                _noteId = value;
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

        [RelayCommand]
        public async Task DeleteNote(Popup popup)
        {

#if DEBUG
            GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Deletando anotação")
                .Property("NoteId", _noteId)
                .Log();
#endif

            if (NoteDAO.DeleteNote(NoteId, NotebookId, UserIdFk))
            {
                await Shell.Current.GoToAsync($"{nameof(NotebookPage)}?PassNotebookId={NotebookId}");
                popup.Close();
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(NotebookPage)}?PassNotebookId={NotebookId}");
                popup.Close();
            }
        }
    }
}

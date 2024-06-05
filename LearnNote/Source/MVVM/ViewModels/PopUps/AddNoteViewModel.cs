
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.Views;
using System.Collections.Specialized;
using System.ComponentModel;

namespace LearnNote.Source.MVVM.ViewModels.PopUps
{
    public partial class AddNoteViewModel : ObservableObject, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        #region Properties
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _title;

        private uint _notebookIdFk;

        private uint _userIdFk;

        #endregion

        #region Getters & Setters
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public uint NotebookIdFk
        {
            get => _notebookIdFk;
            set
            {
                _notebookIdFk = value;
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
        public void AddNote(Popup popup)
        {
            uint noteId;
            noteId = NoteDAO.CreateNote(Title, NotebookIdFk, UserIdFk);

            if (noteId != 0)
            {
                Shell.Current.GoToAsync($"{nameof(NotePage)}?PassNoteId={noteId}");
                popup.Close();
            }
            else
            {
                Shell.Current.GoToAsync($"{nameof(NotebookPage)}?PassNotebookId={NotebookIdFk}");
                popup.Close();
            }
        }
    }
}

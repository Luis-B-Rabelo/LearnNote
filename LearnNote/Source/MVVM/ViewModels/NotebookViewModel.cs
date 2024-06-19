using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Model;
using LearnNote.Source.Core;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.ViewModels.PopUps;
using LearnNote.Source.MVVM.Views;
using NLog;
using System.Collections.ObjectModel;

namespace LearnNote.Source.MVVM.ViewModels
{
    public partial class NotebookViewModel : NavBar, IQueryAttributable
    {
        private readonly IPopupService _popupService;

        private ObservableCollection<NoteModel> _notes;

        public ObservableCollection<NoteModel> Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged("Notes");
            }
        }

        private string _notebookTitle;

        private uint _notebookId;

        private byte _qntNotes;

        public uint NotebookId
        {
            get => _notebookId;
            set
            {
                _notebookId = value;
                OnPropertyChanged("NotebookId");
            }
        }

        public byte QntNotes
        {
            get => _qntNotes;
            private set
            {
                _qntNotes = value;
                OnPropertyChanged();
            }
        }

        public string NotebookTitle
        {
            get => _notebookTitle;
            set
            {
                _notebookTitle = value;
                OnPropertyChanged();
            }
        }

        public uint NoteId { get; private set; }

        public string Title { get; private set; }


        public NotebookViewModel(IPopupService popupService)
        {
            _popupService = popupService;
        }


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("PassNotebookId"))
            {
                NotebookId = uint.Parse(query["PassNotebookId"].ToString());

                NotebookModel notebook = NotebookDAO.SelectNotebook(NotebookId);

                NotebookTitle = notebook.Title;
                QntNotes = notebook.QuantityNotes;
                UserId = notebook.UserIdFk;

                Notes = NoteDAO.SelectNotebookNotes(NotebookId);

#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Parametro passado e puxando anotações")
                    .Property("PassUserId", UserId)
                    .Property("PassNotebookId", NotebookId)
                    .Property("Anotações", Notes)
                    .Log();
#endif
            }
        }

        [RelayCommand]
        public async Task OpenNote(uint noteId)
        {
            await Shell.Current.GoToAsync($"{nameof(NotePage)}?PassNoteId={noteId}");
        }

        [RelayCommand]
        public async Task DisplayAddNotePopUp()
        {
            _popupService.ShowPopup<AddNoteViewModel>(onPresenting: viewModel => { viewModel.UserIdFk = UserId; viewModel.NotebookIdFk = NotebookId; });
        }

        [RelayCommand]
        public async Task DisplayDeleteNotebookPopUp()
        {
            _popupService.ShowPopup<DeleteNotebookViewModel>(onPresenting: viewModel => { viewModel.NotebookTitle = _notebookTitle; viewModel.UserIdFk = UserId; viewModel.NotebookId = NotebookId;  });
        }
    }
}

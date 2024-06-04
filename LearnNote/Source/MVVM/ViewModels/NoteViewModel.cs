using CommunityToolkit.Mvvm.Input;
using LearnNote.Model;
using LearnNote.Source.Core;
using LearnNote.Source.DAO;
using NLog;

namespace LearnNote.Source.MVVM.ViewModels
{
    public partial class NoteViewModel : NavBar, IQueryAttributable
    {
        private uint _noteId;

        private uint _notebookId;

        private string _title;

        private string _text;

        private DateOnly _creationDate;

        private DateTime _lastEditDateTime;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("PassNoteId"))
            {
                _noteId = uint.Parse(query["PassNoteId"].ToString());

                NoteModel note = NoteDAO.SelectNote(_noteId);

                Title = note.Title;
                Text = note.Text;
                _notebookId = note.NotebookId;
                _creationDate = note.CreationDate;
                _lastEditDateTime = note.LastEditDateTime;
                UserId = note.UserId;

#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Parametro passado e puxando anotação")
                    .Property("PassUserId", UserId)
                    .Property("NoteId", _noteId)
                    .Property("Cadernos", Title)
                    .Log();
#endif

                note = null;
            }
        }

        [RelayCommand]
        public async Task SaveNote()
        {
            if(NoteDAO.SaveNote(UserId, _notebookId, _noteId, Text))
            {
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Anotação Salva")
                    .Property("NoteId", _noteId)
                    .Property("Anotação", Text)
                    .Log();
            }
            else
            {
                GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Problema para salvar anotação")
                .Property("NoteId", _noteId)
                .Property("Anotação", Text)
                .Log();
            }

        }


    }
}

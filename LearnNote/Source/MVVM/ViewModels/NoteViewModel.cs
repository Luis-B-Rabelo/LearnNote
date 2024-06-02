using LearnNote.Model;
using LearnNote.Source.Core;
using LearnNote.Source.DAO;
using NLog;

namespace LearnNote.Source.MVVM.ViewModels
{
    public partial class NoteViewModel : NavBar, IQueryAttributable
    {
        private uint _noteId;

        private string _title;

        private string _text;

        private DateOnly _creationDate;

        private DateTime _lastEditDateTime;

        public uint NoteId
        {
            get => _noteId;
            set => _noteId = value;
        }

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

        public DateOnly CreationDate
        {
            get => _creationDate;
            set
            {
                _creationDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime LastEditDateTime
        {
            get => _lastEditDateTime;
            set
            {
                _lastEditDateTime = value;
                OnPropertyChanged();
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("PassNoteId") && query.ContainsKey("PassUserId"))
            {
                NoteId = uint.Parse(query["PassNoteId"].ToString());
                UserId = uint.Parse(query["PassUserId"].ToString());

                NoteModel note = NoteDAO.SelectNote(NoteId, UserId);

                Title = note.Title;
                Text = note.Text;
                CreationDate = note.CreationDate;
                LastEditDateTime = note.LastEditDateTime;

#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Parametro passado e puxando anotação")
                    .Property("PassUserId", UserId)
                    .Property("NoteId", NoteId)
                    .Property("Cadernos", Title)
                    .Log();
#endif

            }
        }

    }
}

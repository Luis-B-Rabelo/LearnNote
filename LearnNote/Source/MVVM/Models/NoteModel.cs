namespace LearnNote.Model
{
    public class NoteModel
    {
        #region Properties
        private uint _noteId; //(composed) vai juntar 2 IDs
        private uint _notebookId;
        private string _title;
        private string? _text;
        private DateOnly _creationDate;
        private DateTime _lastEditDateTime;
        #endregion

        #region Getters & Setters

        public uint NoteId 
        { 
            get => _noteId; 
            set => _noteId = value;  
        }

        public uint NotebookId 
        { 
            get => _notebookId;  
            set => _notebookId = value;  
        }

        public string Title
        {
            get => _title; 
            set => _title = value; 
        }

        public string? Text
        {
            get => _text;
            set => _text = value;
        }

        public DateOnly CreationDate
        {
            get => _creationDate; 
            set => _creationDate = value; 
        }

        public DateTime LastEditDateTime
        {
            get => _lastEditDateTime; 
            set => _lastEditDateTime = value; 
        }

        #endregion
    }
}

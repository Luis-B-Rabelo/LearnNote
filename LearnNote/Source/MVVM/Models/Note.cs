namespace LearnNote.Model
{
    public class Note
    {
        #region "Variáveis"
        private uint _noteId; //(composed) vai juntar 2 IDs
        private uint _notebookId;
        private string _title;
        private string _text;
        private DateOnly _createDate;
        private DateOnly _lastEditDate;
        #endregion

        #region "Propriedades"

        public uint NoteId 
        { 
            get { return _noteId; }
            set { _noteId = value; } 
        }

        public uint NotebookId 
        { 
            get { return _notebookId; } 
            set { _notebookId = value; } 
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public DateOnly CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        public DateOnly LastEditDate
        {
            get { return _lastEditDate; }
            set { _lastEditDate = value; }
        }

        #endregion
    }
}

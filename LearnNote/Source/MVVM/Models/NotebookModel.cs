using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNote.Model
{
    public class NotebookModel
    {
        #region Properties

        private uint _notebookId;
        private uint _userId;
        private string _title;
        private ushort _quantityNotes;

        #endregion

        #region Getters & Setters

        public uint NotebookId
        {
            get { return _notebookId; }
            set { _notebookId = value; }
        }

        public uint UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public ushort QuantityNotes
        {
            get { return _quantityNotes; }
            set { _quantityNotes = value; }
        }

        #endregion
    }
}
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
        private uint _userIdFk;
        private string _title;
        private ushort _quantityNotes;

        #endregion

        #region Getters & Setters

        public uint NotebookId
        {
            get => _notebookId;
            set => _notebookId = value;
        }

        public uint UserIdFk
        {
            get => _userIdFk;
            set => _userIdFk = value;
        }

        public string Title
        {
            get => _title; 
            set => _title = value;
        }

        public ushort QuantityNotes
        {
            get => _quantityNotes;
            set => _quantityNotes = value;
        }

        #endregion
    }
}
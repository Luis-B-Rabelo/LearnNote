using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNote.Model
{
    public class Note
    {
        private uint noteId; //(composed) vai juntar 2 IDs
        private uint notebookId;
        private string title;
        private string text;
        private DateOnly createDate;
        private DateOnly lastEditDate;
    }
}

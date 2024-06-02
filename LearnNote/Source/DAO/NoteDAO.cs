
using LearnNote.Model;
using NLog;
using System.Collections.ObjectModel;

namespace LearnNote.Source.DAO
{
    public  class NoteDAO : BaseDAO
    {

        public static uint CreateNote(string title, uint notebookIdFk, uint userIdFk)
        {
            Dictionary<string, object> note = new Dictionary<string, object>
            {
                { "noteTitle", title },
                { "noteCreationDate", DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd") },
                { "noteLastEditDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                { "notebookIdFk", notebookIdFk }
            };

            Dictionary<string, object> noteSearch = new Dictionary<string, object>
            {
                { "noteTitle", title },
                { "notebookIdFk", notebookIdFk }
            };

            string[] specifics = { "noteId" };

            List<Dictionary<string, object>> elements = new List<Dictionary<string, object>>();

            try
            {
                elements = SelectWholeByProperties("notetable", noteSearch);

                if (elements == null)
                {
                    if (InsertInto("notetable", note))
                    {
                        elements = SelectSpecificsByProperties("notetable", specifics, noteSearch);

                        string path = $@"{AppDomain.CurrentDomain.BaseDirectory}\Storage\Users\{userIdFk}\Notebooks\{notebookIdFk}";

                        File.WriteAllText(Path.Combine(path, $"{(uint)elements.First()["noteId"]}.txt"), "");

#if DEBUG
                        GlobalFunctionalities.Logger.ForDebugEvent()
                            .Message("Criando nova anotação no caderno")
                            .Property("Caderno", notebookIdFk)
                            .Property("Anotação", (uint)elements.First()["noteId"])
                            .Log();
#endif


                        return (uint)elements.First()["noteId"];
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static ObservableCollection<NoteModel> SelectNotebookNotes(uint notebookIdFk)
        {
            ObservableCollection<NoteModel> notes = new ObservableCollection<NoteModel>();

            Dictionary<string, object> search = new Dictionary<string, object>
            {
                { "notebookIdFk", notebookIdFk },
            };

            List<Dictionary<string, object>> elements = SelectWholeByProperties("notetable", search);

            if (elements != null)
            {
                foreach (Dictionary<string, object> element in elements)
                {
                    notes.Add(new NoteModel
                    {
                        NoteId = (uint)element["noteId"],
                        Title = (string)element["noteTitle"],
                        CreationDate = DateOnly.FromDateTime((DateTime)element["noteCreationDate"]),
                        LastEditDateTime = (DateTime)element["noteLastEditDateTime"],
                        NotebookId = notebookIdFk,
                    });
                }
            }

            return notes;
        }

        public static void SaveNote(uint userIdFk, uint notebookIdFk, uint noteId, string[] content)
        {
            string path = $@"{AppDomain.CurrentDomain.BaseDirectory}\Storage\Users\{userIdFk}\Notebooks\{notebookIdFk}";

            File.AppendAllLines(Path.Combine(path, $"{noteId}.txt"), content);

        }

        public static NoteModel? SelectNote(uint noteId, uint userIdFk)
        {
            try
            {
                StreamReader reader;

                string text = string.Empty;

                string path = string.Empty;

                NoteModel note;

                Dictionary<string, object> search = new Dictionary<string, object>
                {
                    { "noteId", noteId }
                };

                Dictionary<string, object> noteTable = SelectWholeByProperties("notetable", search).First();

                note = new NoteModel
                {
                    NoteId = noteId,
                    NotebookId = (uint)noteTable["notebookIdFk"],
                    Title = (string)noteTable["noteTitle"],
                    CreationDate = DateOnly.FromDateTime((DateTime)noteTable["noteCreationDate"]),
                    LastEditDateTime = (DateTime)noteTable["noteLastEditDateTime"]
                };

                path = $@"{AppDomain.CurrentDomain.BaseDirectory}\Storage\Users\{userIdFk}\Notebooks\{note.NotebookId}";

                reader = new(Path.Combine(path, $"{noteId}.txt"));

                text = reader.ReadToEnd();

                note.Text = text;

                return note;
            }
            catch (IOException ex)
            {
                GlobalFunctionalities.Logger.Error("Erro na hora de selecionar a anotação", ex);
                return null;
            }
        }
    }
}

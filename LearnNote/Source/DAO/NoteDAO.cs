
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
                { "userIdFk", userIdFk },
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

                        File.Create(Path.Combine(path, $"{(uint)elements.First()["noteId"]}.txt"));

#if DEBUG
                        GlobalFunctionalities.Logger.ForDebugEvent()
                            .Message("Criando nova anotação no caderno")
                            .Property("Caderno", notebookIdFk)
                            .Property("Anotação", (uint)elements.First()["noteId"])
                            .Log();
#endif

                        string[] countNotes = { "noteId" };

                        noteSearch = new Dictionary<string, object>
                        {
                            { "notebookIdFk", notebookIdFk }
                        };

                        Dictionary<string, object> notebookSearch = new Dictionary<string, object>
                        {
                            { "notebookId", notebookIdFk }
                        };


                        List<Dictionary<string, object>> listNotes = SelectSpecificsByProperties("notetable", countNotes, noteSearch);

                        byte qntNotes;

                        if (listNotes != null)
                        {
                            qntNotes = (byte)listNotes.Count();
                        }
                        else
                        {
                            qntNotes= 0;
                        }

                        Dictionary<string, object> updateQntNotes = new Dictionary<string, object>
                        {
                            { "notebookQntNotes", qntNotes }
                        };

                        UpdateByProperties("notebooktable", updateQntNotes, notebookSearch);

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

        public static bool DeleteNote(uint noteId, uint notebookIdFk, uint userIdFk)
        {
            Dictionary<string, object> note = new Dictionary<string, object>
            {
                { "noteId", noteId }
            };

            try
            {

                if (DeleteByProperties("notetable", note))
                {
                    string path = $@"{AppDomain.CurrentDomain.BaseDirectory}\Storage\Users\{userIdFk}\Notebooks\{notebookIdFk}";

                    File.Delete(Path.Combine(path, $"{noteId}.txt"));

#if DEBUG
                    GlobalFunctionalities.Logger.ForDebugEvent()
                        .Message("Deletando anotação no caderno")
                        .Property("Caderno", notebookIdFk)
                        .Property("Anotação", noteId)
                        .Log();
#endif

                    string[] countNotes = { "noteId" };

                    Dictionary<string, object> noteSearch = new Dictionary<string, object>
                    {
                        { "notebookIdFk", notebookIdFk }
                    };

                    Dictionary<string, object> notebookSearch = new Dictionary<string, object>
                    {
                        { "notebookId", notebookIdFk }
                    };

                    List<Dictionary<string, object>> listNotes = SelectSpecificsByProperties("notetable", countNotes, noteSearch);

                    byte qntNotes;

                    if (listNotes != null)
                    {
                        qntNotes = (byte)listNotes.Count();
                    }
                    else
                    {
                        qntNotes = 0;
                    }

                    Dictionary<string, object> updateQntNotes = new Dictionary<string, object>
                    {
                        { "notebookQntNotes", qntNotes }
                    };

                    UpdateByProperties("notebooktable", updateQntNotes, notebookSearch);

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                return false;
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
                        UserId = (uint)element["userIdFk"],
                        NotebookId = notebookIdFk,
                    });
                }
            }

            return notes;
        }

        public static ObservableCollection<NoteModel> SelectRecentNotebookNotes(uint userIdFk)
        {
            ObservableCollection<NoteModel> notes = new ObservableCollection<NoteModel>();

            Dictionary<string, object> search = new Dictionary<string, object>
            {
                { "userIdFk", userIdFk },
            };

            Dictionary<string, bool> order = new Dictionary<string, bool>
            {
                { "noteLastEditDateTime", false },
            };

            List<Dictionary<string, object>> elements = SelectSomeOrdenedByProperties("notetable", search, 4, order);

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
                        UserId = userIdFk,
                        NotebookId = (uint)element["notebookIdFk"],
                    });
                }
            }

            return notes;
        }
        public static bool SaveNote(uint userIdFk, uint notebookIdFk, uint noteId, string content)
        {
            try
            {
                string path = $@"{AppDomain.CurrentDomain.BaseDirectory}\Storage\Users\{userIdFk}\Notebooks\{notebookIdFk}";

                File.WriteAllText(Path.Combine(path, $"{noteId}.txt"), content);

                try
                {
                    Dictionary<string, object> update = new Dictionary<string, object>
                    {
                        { "noteLastEditDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
                    };

                    Dictionary<string, object> where = new Dictionary<string, object>
                    {
                        { "noteId", noteId}
                    };

                    if(BaseDAO.UpdateByProperties("notetable", update, where))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                } 
                catch(Exception ex) 
                { 
                    GlobalFunctionalities.Logger.Error(ex);
                    return false;
                }
            }
            catch(IOException ioEx)
            {
                GlobalFunctionalities.Logger.Error(ioEx);
                return false;
            }


        }

        public static NoteModel? SelectNote(uint noteId)
        {
            try
            {
                string text = string.Empty;

                string path = string.Empty;

                NoteModel note;

                Dictionary<string, object> search = new Dictionary<string, object>
                {
                    { "noteId", noteId }
                };

                Dictionary<string, object> noteTable = SelectWholeByProperties("notetable", search).ElementAt(0);

                note = new NoteModel
                {
                    NoteId = noteId,
                    NotebookId = (uint)noteTable["notebookIdFk"],
                    UserId = (uint)noteTable["userIdFk"],
                    Title = (string)noteTable["noteTitle"],
                    CreationDate = DateOnly.FromDateTime((DateTime)noteTable["noteCreationDate"]),
                    LastEditDateTime = (DateTime)noteTable["noteLastEditDateTime"]
                };

                path = $@"{AppDomain.CurrentDomain.BaseDirectory}\Storage\Users\{note.UserId}\Notebooks\{note.NotebookId}";

                text = File.ReadAllText(Path.Combine(path, $"{noteId}.txt"));

                note.Text = text;

                return note;
            }
            catch (IOException ex)
            {
                GlobalFunctionalities.Logger.ForErrorEvent()
                    .Message("Erro na hora de ler anotação")
                    .Property("Exceção", ex)
                    .Log();

                return null;
            }
        }
    }
}

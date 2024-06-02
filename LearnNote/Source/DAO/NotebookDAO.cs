using LearnNote.Model;
using NLog;
using System.Collections.ObjectModel;

namespace LearnNote.Source.DAO
{
    public class NotebookDAO : BaseDAO
    {
        public static uint CreateNotebook(string title, uint userIdFk) 
        {
#if DEBUG
            GlobalFunctionalities.Logger.Debug("Começando processo de ciração de caderno");
#endif
            Dictionary<string, object> notebook = new Dictionary<string, object>
            {
                { "notebookTitle", title },
                { "userIdFk", userIdFk }
            };

            string[] specifics = { "notebookId" };

            List<Dictionary<string, object>> elements = new List<Dictionary<string, object>>();

            try
            {
                elements = SelectWholeByProperties("notebooktable", notebook);
#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Testando se caderno já existe")
                    .Property("Resultado", elements)
                    .Log();
#endif
                if (elements == null)
                {
                    if (InsertInto("notebooktable", notebook))
                    {
#if DEBUG
                        GlobalFunctionalities.Logger.Debug("Caderno salvo no BD");
#endif
                        elements = SelectSpecificsByProperties("notebooktable", specifics, notebook);

#if DEBUG
                        GlobalFunctionalities.Logger.ForDebugEvent()
                            .Message("Pegando Id do caderno")
                            .Property("Resultado", elements)
                            .Log();
#endif

                        if (elements != null)
                        {
                            Directory.CreateDirectory($@"{AppDomain.CurrentDomain.BaseDirectory}\Storage\Users\{userIdFk}\Notebooks\{(uint)elements.First()["notebookId"]}");
#if DEBUG
                            GlobalFunctionalities.Logger.ForDebugEvent()
                                .Message("Criando diretório de armazenamento de caderno")
                                .Property("Caderno", (uint)elements.First()["notebookId"])
                                .Log();
#endif

                            return (uint)elements.First()["notebookId"];
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

        public static NotebookModel SelectNotebook(uint notebookId)
        {
            NotebookModel notebook = new NotebookModel();

            Dictionary<string, object> search = new Dictionary<string, object>
            {
                { "notebookId", notebookId }
            };

            List<Dictionary<string, object>> elements = SelectWholeByProperties("notebooktable", search);

            if (elements != null)
            {
                notebook = new NotebookModel
                {
                    NotebookId = notebookId,
                    Title = (string)elements.First()["notebookTitle"],
                    QuantityNotes = (ushort)elements.First()["notebookQntNotes"],
                    UserIdFk = (uint)elements.First()["userIdFk"]
                };
            }

            return notebook;
        }

        public static ObservableCollection<NotebookModel> SelectUserNotebooks(uint userIdFk)
        {
            ObservableCollection<NotebookModel> notebooks = new ObservableCollection<NotebookModel>();

            Dictionary<string, object> search = new Dictionary<string, object>
            {
                { "userIdFk", userIdFk }
            };

            List<Dictionary<string, object>> elements = SelectWholeByProperties("notebooktable", search);

            if (elements != null)
            {
                foreach (Dictionary<string, object> element in elements)
                {
                    notebooks.Add(new NotebookModel
                    {
                        NotebookId = (uint)element["notebookId"],
                        Title = (string)element["notebookTitle"],
                        QuantityNotes = (ushort)element["notebookQntNotes"],
                        UserIdFk = userIdFk
                    });
                }
            }

            return notebooks;
        }

        
    }
}

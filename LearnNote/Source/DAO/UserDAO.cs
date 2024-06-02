using LearnNote.Model;
using MySql.Data.MySqlClient;
using NLog;

namespace LearnNote.Source.DAO
{
    public class UserDAO : BaseDAO
    {
        public static bool CreateNewUser(string email, string userName, string password)
        {

            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "userEmail", email },
                { "userName", userName },
                { "userPassword", password }
            };

            string[] specifics = { "userId" };

            try
            {
                if(InsertInto("usertable", user))
                {
                    List<Dictionary<string, object>> elements = SelectSpecificsByProperties("usertable", specifics, user);


                        Directory.CreateDirectory($@"{AppDomain.CurrentDomain.BaseDirectory}\Storage\Users\{(uint)elements.First()["userId"]}\Notebooks");
#if DEBUG
                        GlobalFunctionalities.Logger.ForDebugEvent()
                            .Message("Criando diretório de armazenamento de usuário")
                            .Property("Usuário", (uint)elements.First()["userId"])
                            .Log();
#endif
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

        public static bool ConfirmUser(string email, string password)
        {

            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "userEmail", email },
                { "userPassword", password }
            };

            List<Dictionary<string, object>> elements = SelectWholeByProperties("usertable", user);

            if (elements != null)
            {
                if (!(Directory.Exists($@"{AppDomain.CurrentDomain.BaseDirectory}\Storage\Users\{(uint)elements.First()["userId"]}")))
                {
                    Directory.CreateDirectory($@"{AppDomain.CurrentDomain.BaseDirectory}\Storage\Users\{(uint)elements.First()["userId"]}\Notebooks");
#if DEBUG
                    GlobalFunctionalities.Logger.ForDebugEvent()
                        .Message("Criando diretório de armazenamento de usuário")
                        .Property("Usuário", (uint)elements.First()["userId"])
                        .Log();
#endif
                }

#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Confirmação de usuário")
                    .Property("Email", email)
                    .Property("Senha", password)
                    .Log();
#endif

                return true;
            }
            else
            {
#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Erro na confirmação de usuário")
                    .Property("Email", email)
                    .Property("Senha", password)
                    .Log();
#endif
                return false;
            }
        }

        public static UserModel SelectUser(string email)
        {
            UserModel user = new UserModel();

            Dictionary<string, object> search = new Dictionary<string, object>
            {
                { "userEmail", email },
            };

            List<Dictionary<string, object>> elements = SelectWholeByProperties("usertable", search);


            user.UserId = (uint)elements.First()["userId"];
            user.UserName = (string)elements.First()["userName"];


            return user;
        }
    }
}

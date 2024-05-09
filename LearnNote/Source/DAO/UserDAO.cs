using LearnNote.Model;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using NLog;

namespace LearnNote.Source.DAO
{
    public class UserDAO : BaseDAO
    {
        public UserDAO() : base() 
        { 

        }

        public bool CreateNewUser(string email, string userName, string password)
        {
            bool result = new bool();

            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "emailUser", email },
                { "nameUser", userName },
                { "passwordUser", password }
            };

            try
            {
                InsertInto("usertable", user);
                MySqlDataReader rdr = SelectSpecificByProperties("usertable", user).ExecuteReader();

                if (rdr.Read())
                {
                    if (rdr["emailUser"] is not null && rdr["passwordUser"] is not null)
                        result = true;
                    else
                        result = false;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex) 
            { 
                    result = false;
            }

            return result;
        }

        public bool ConfirmUser(string email, string password)
        {
            bool result = false;

            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "emailUser", email },
                { "passwordUser", password }
            };

            MySqlDataReader rdr = SelectSpecificByProperties("usertable", user).ExecuteReader();
            if (rdr.Read())
            {
                if (rdr["emailUser"] is not null && rdr["passwordUser"] is not null)
                    result = true;
                else
                    result = false;
            }
            else
            {
                result = false;
            }


#if DEBUG
            GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Confirmação de usuário")
                .Property("Email", rdr["emailUser"])
                .Property("Email", rdr["passwordUser"])
                .Log();
#endif

            return result;
        }

        public UserModel SelectUser(string email)
        {
            UserModel user = new UserModel();

            Dictionary<string, object> search = new Dictionary<string, object>
            {
                { "emailUser", email },
            };

            MySqlDataReader rdr = SelectSpecificByProperties("usertable", search).ExecuteReader();
            if (rdr.Read())
            {
                user.UserId = (uint)rdr["userId"];
                user.UserName = rdr["NameUser"].ToString();
            }

            return user;
        }
    }
}

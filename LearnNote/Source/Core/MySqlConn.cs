using MySql.Data.MySqlClient;
using NLog;

namespace LearnNote.Source.Core
{
    public class MySqlConn
    {
        //Propriedade da conexão

        //Método construtor
        public static MySqlConnection? OpenConn()
        {
            string dbServer = "127.0.0.1";
            string dbPort = "3306";
            string dbUId = "root";
            string dbPassword = "";
            string DB = "learnnote";

            try
            {
                MySqlConnection conn;

                //Tentando criar a conexão
                conn = new MySqlConnection();
                conn.ConnectionString = $"server={dbServer};port={dbPort};uid={dbUId};pwd={dbPassword};database={DB};Allow User Variables=True;";
                conn.Open();


#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Criando conexão com DB")
                    .Property("Server", dbServer)
                    .Property("Port", dbPort)
                    .Property("UserId", dbUId)
                    .Property("Senha", dbPassword)
                    .Property("DB", DB)
                    .Log();
#endif
                return conn;
            }
            catch (MySqlException ex)
            {
                GlobalFunctionalities.Logger.ForErrorEvent()
                .Message("Erro para criar conexão com o DB")
                .Property("Server", dbServer)
                .Property("Port", dbPort)
                .Property("UserId", dbUId)
                .Property("Senha", dbPassword)
                .Property("DB", DB)
                .Exception(ex)
                .Log();

                return null;
            }
        }

        //Método para executar os comandos SQL
        public static bool RunCommand(string sql)
        {
            try
            {
                MySqlConnection conn = OpenConn();
#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Rodando comando SQL")
                    .Property("SQL", sql)
                    .Log();
#endif
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                conn.Close();

                return true;
            }
            catch (MySqlException ex)
            {
                GlobalFunctionalities.Logger.ForErrorEvent()
                    .Message("Erro para rodar comando SQL")
                    .Property("SQL", sql)
                    .Exception(ex)
                    .Log();
                return false;
            }
        }

        //Método para teste da conexão com BD
        public static bool TestConnection()
        {
            try
            {
                MySqlConnection conn = OpenConn();

                bool result = new bool();

                string sql2 = "SELECT * FROM testtable;";

#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Rodando comando SQL")
                    .Property("SQL2", sql2)
                    .Log();
#endif
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);

                MySqlDataReader rdr = cmd2.ExecuteReader();

                if (rdr.Read())
                {
                    GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Testando conexão com DB")
                    .Property("Read", rdr[0])
                    .Log();
                    result = true;
                }
                rdr.Close();

                conn.Close();

                return result;
            }
            catch (MySqlException ex)
            {

                GlobalFunctionalities.Logger.ForErrorEvent()
                    .Message("Erro na conexão")
                    .Property("Error Number", ex.Number)
                    .Exception(ex)
                    .Log();

                return false;
            }

        }
    }
}

using MySql.Data.MySqlClient;
using NLog;

namespace LearnNote.Source.DAO
{
    public class MySqlConn
    {
        //Propriedades para conexão com o BD
        private string _dbServer = "127.0.0.1";
        private string _dbPort = "3306";
        private string _dbUId = "root";
        private string _dbPassword = "";
        private string _DB = "learnnote";

        //Propriedade da conexão
        private MySqlConnection _conn;

        //Método construtor
        public MySqlConn()
        {
            try
            {
                //Tentando criar a conexão
                _conn = new MySqlConnection();
                _conn.ConnectionString = $"server={_dbServer};port={_dbPort};uid={_dbUId};pwd={_dbPassword};database={_DB};Allow User Variables=True;";
                _conn.Open();

                
#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Criando conexão com DB")
                    .Property("Server", _dbServer)
                    .Property("Port", _dbPort)
                    .Property("UserId", _dbUId)
                    .Property("Senha", _dbPassword)
                    .Property("DB", _DB)
                    .Log();
#endif

            }
            catch (MySqlException ex)
            {
                GlobalFunctionalities.Logger.ForErrorEvent()
                .Message("Erro para criar conexão com o DB")
                .Property("Server", _dbServer)
                .Property("Port", _dbPort)
                .Property("UserId", _dbUId)
                .Property("Senha", _dbPassword)
                .Property("DB", _DB)
                .Exception(ex)
                .Log();
            }
        }

        //Método deconstrutor
        ~MySqlConn()
        {
            try
            {
                _conn.Close();
#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Fechando conexão com DB")
                    .Property("Server", _dbServer)
                    .Property("Port", _dbPort)
                    .Property("UserId", _dbUId)
                    .Property("Senha", _dbPassword)
                    .Property("DB", _DB)
                    .Log();
#endif

            }
            catch (MySqlException ex)
            {
                GlobalFunctionalities.Logger.ForErrorEvent()
                .Message("Erro para fechar conexão com o DB")
                .Property("Server", _dbServer)
                .Property("Port", _dbPort)
                .Property("UserId", _dbUId)
                .Property("Senha", _dbPassword)
                .Property("DB", _DB)
                .Exception(ex)
                .Log();
            }
        }

        //Método para executar os comandos SQL
        public MySqlCommand? RunCommand(string sql)
        {
            try
            {

#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Rodando comando SQL")
                    .Property("SQL", sql)
                    .Log();
#endif
                MySqlCommand cmd = new MySqlCommand(sql, _conn);
                cmd.ExecuteNonQuery();

                return cmd;
            }
            catch (MySqlException ex)
            {
                GlobalFunctionalities.Logger.ForErrorEvent()
                    .Message("Erro para rodar comando SQL")
                    .Property("SQL", sql)
                    .Exception(ex)
                    .Log();
                return null;
            }
        }

        //Método para teste da conexão com BD
        public bool TestConnection()
        {
            try
            {
                bool result = new bool();

                string sql1 = "INSERT INTO `testtable` (`testId`) VALUES (null);";
                string sql2 = "SELECT * FROM testtable;";
                
#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Rodando comando SQL")
                    .Property("SQL1", sql1)
                    .Property("SQL2", sql2)
                    .Log();
#endif
                MySqlCommand cmd1 = new MySqlCommand(sql1, _conn);
                cmd1.ExecuteNonQuery();
                MySqlCommand cmd2 = new MySqlCommand(sql2, _conn);

                MySqlDataReader rdr = cmd2.ExecuteReader();

                if(rdr.Read())
                {
                    GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Testando conexão com DB")
                    .Property("Read", rdr[0])
                    .Log();
                    result = true;
                }
                rdr.Close();

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

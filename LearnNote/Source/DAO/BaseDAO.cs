using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNote.Source.DAO
{
    public class BaseDAO 
    {
        private MySqlConn _mysqlConn;

        public BaseDAO() 
        {
            _mysqlConn = new MySqlConn();
        }

        protected void InsertInto(string table, Dictionary<string, object> properties)
        {
            try
            {
                MySqlCommand? cmd;
                string column = string.Empty;
                string values = string.Empty;

                if (properties.Count > 1)
                {
                    byte counter = 0;
                    foreach (KeyValuePair<string, object> property in properties)
                    {
                        counter++;
                        if (counter < properties.Count)
                        {
                            column += $"{property.Key}, ";
                            values += $"'{property.Value}', ";
                        }
                        else
                        {
                            column += $"{property.Key} ";
                            values += $"'{property.Value}'";
                        }

                    }
                }
                else
                {
                    column += $"'{properties.ElementAt(0).Key}' ";
                    values += $"'{properties.ElementAt(0).Value}' ";
                }

                string sql = $"INSERT INTO {table} ({column}) VALUES ({values});";

                cmd = _mysqlConn.RunCommand(sql);

                if (cmd is not null)
                {
#if DEBUG
                    GlobalFunctionalities.Logger.ForDebugEvent()
                        .Message("Inserindo novo elemento")
                        .Property("Tabela", table)
                        .Property("Colunas", properties.Keys)
                        .Property("Valores", properties.Values)
                        .Log();
#endif
                }
                else
                {
#if DEBUG
                    GlobalFunctionalities.Logger.ForDebugEvent()
                        .Message("Problema para rodar comando de inserir novo elemento")
                        .Property("Tabela", table)
                        .Property("Colunas", properties.Keys)
                        .Property("Valores", properties.Values)
                        .Log();
#endif
                }
 
            }
            catch (Exception ex)
            {
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Problema inserir novo elemento")
                    .Property("Tabela", table)
                    .Property("Colunas", properties.Keys)
                    .Property("Valores", properties.Values)
                    .Log();
            }
        }

        protected MySqlCommand SelectAll(string table)
        {
            try
            {
                MySqlCommand? cmd;
                string sql = $"SELECT * FROM `{table}`;";
                cmd = _mysqlConn.RunCommand(sql);

                if (cmd is not null)
                    return cmd;
                else
                    return null;
            }
            catch (Exception ex)
            {
                GlobalFunctionalities.Logger.ForErrorEvent()
                    .Message("Problema para selecionar elemento/s")
                    .Property("Tabela", table)
                    .Exception(ex)
                    .Log();
                return null;
            }



        }

        protected MySqlCommand SelectSpecificByProperties(string table, Dictionary<string, object> properties)
        {
            try
            {
                MySqlCommand? cmd;
                string where = string.Empty;

                if (properties.Count > 1)
                {
                    byte counter = 0;
                    foreach (KeyValuePair<string, object> property in properties)
                    {
                        counter++;
                        if (counter < properties.Count)
                        {
                            where += $"{property.Key} = '{property.Value}' AND ";
                        }
                        else
                        {
                            where += $"{property.Key} = '{property.Value}'";
                        }

                    }
                }
                else
                {
                    where = $"{properties.ElementAt(0).Key} = '{properties.ElementAt(0).Value}'";
                }

                string sql = $"SELECT * FROM {table} WHERE {where};";
                cmd = _mysqlConn.RunCommand(sql);

#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Selecionando elemento/s")
                    .Property("Tabela", table)
                    .Property("Colunas de pesquisa", properties.Keys)
                    .Property("Valores de pesquisa", properties.Values)
                    .Log();
#endif
                if (cmd is not null)
                    return cmd;
                else
                    return null;

            }
            catch (Exception ex)
            {
                GlobalFunctionalities.Logger.ForErrorEvent()
                    .Message("Problema para selecionar elemento/s")
                    .Property("Tabela", table)
                    .Property("Colunas de pesquisa", properties.Keys)
                    .Property("Valores de pesquisa", properties.Values)
                    .Exception(ex)
                    .Log();

                return null;
            }

        }
    }
}

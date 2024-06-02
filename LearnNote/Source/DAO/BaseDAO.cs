using LearnNote.Source.Core;
using MySql.Data.MySqlClient;
using NLog;

namespace LearnNote.Source.DAO
{
    public class BaseDAO
    {
        protected static bool InsertInto(string table, Dictionary<string, object> properties)
        {
            try
            {
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

                if (MySqlConn.RunCommand(sql))
                {
#if DEBUG
                    GlobalFunctionalities.Logger.ForDebugEvent()
                        .Message("Inserindo novo elemento")
                        .Property("Tabela", table)
                        .Property("Colunas", properties.Keys)
                        .Property("Valores", properties.Values)
                        .Log();
#endif
                    return true;
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
                    return false;
                }

            }
            catch (Exception ex)
            {
                GlobalFunctionalities.Logger.ForErrorEvent()
                    .Message("Problema inserir novo elemento")
                    .Property("Tabela", table)
                    .Property("Colunas", properties.Keys)
                    .Property("Valores", properties.Values)
                    .Exception(ex)
                    .Log();

                return false;
            }
        }

        protected static bool DeleteByProperties(string table, Dictionary<string, object> properties)
        {
            try
            {
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

                string sql = $"DELETE FROM {table} WHERE {where};";

                if (MySqlConn.RunCommand(sql))
                {
#if DEBUG
                    GlobalFunctionalities.Logger.ForDebugEvent()
                            .Message("Deletando elemento/s")
                            .Property("Tabela", table)
                            .Property("Colunas de pesquisa", properties.Keys)
                            .Property("Valores de pesquisa", properties.Values)
                            .Log();
#endif

                    return true;
                }
                else
                {
#if DEBUG
                    GlobalFunctionalities.Logger.ForDebugEvent()
                        .Message("Problema para rodar comando para deletar elemento/s")
                        .Property("Tabela", table)
                        .Property("Colunas de pesquisa", properties.Keys)
                        .Property("Valores de pesquisa", properties.Values)
                        .Log();
#endif

                    return false;
                }
            }
            catch (Exception ex)
            {
                GlobalFunctionalities.Logger.ForErrorEvent()
                    .Message("Problema para deletar elemento/s")
                    .Property("Tabela", table)
                    .Property("Colunas de pesquisa", properties.Keys)
                    .Property("Valores de pesquisa", properties.Values)
                    .Exception(ex)
                    .Log();

                return false;
            }

        }

        protected static bool UpdateByProperties(string table, Dictionary<string, object> properties, Dictionary<string, object> whereProperties)
        {
            try
            {
                string columnValue = string.Empty;
                string where = string.Empty;

                byte counter;

                //Arrumando string da parte de "SET" do comando de "UPDATE"
                if (properties.Count > 1)
                {
                    counter = 0;

                    foreach (KeyValuePair<string, object> property in properties)
                    {
                        counter++;
                        if (counter < properties.Count)
                        {
                            columnValue += $"{property.Key} = '{property.Value}', ";
                        }
                        else
                        {
                            columnValue += $"{property.Key} = '{property.Value}'";
                        }

                    }
                }
                else
                {
                    columnValue += $"{properties.ElementAt(0).Key} = '{properties.ElementAt(0).Value}'";
                }

                //Arrumando string da parte de "WHERE" do comando de "UPDATE"
                if (whereProperties.Count > 1)
                {
                    counter = 0;

                    foreach (KeyValuePair<string, object> condition in whereProperties)
                    {
                        counter++;
                        if (counter < whereProperties.Count)
                        {
                            where += $"{condition.Key} = '{condition.Value}' AND ";
                        }
                        else
                        {
                            where += $"{condition.Key} = '{condition.Value}'";
                        }

                    }
                }
                else
                {
                    where = $"{whereProperties.ElementAt(0).Key} = '{whereProperties.ElementAt(0).Value}'";
                }

                string sql = $"UPDATE {table} SET {columnValue} WHERE {where};";

                if (MySqlConn.RunCommand(sql))
                {
#if DEBUG
                    GlobalFunctionalities.Logger.ForDebugEvent()
                        .Message("Atualizando tabela")
                        .Property("Tabela", table)
                        .Property("Colunas", properties.Keys)
                        .Property("Valores", properties.Values)
                        .Property("Colunas de pesquisa", whereProperties.Keys)
                        .Property("Valores de pesquisa", whereProperties.Values)
                        .Log();
#endif

                    return true;
                }
                else
                {
#if DEBUG
                    GlobalFunctionalities.Logger.ForDebugEvent()
                        .Message("Problemas para rodar comando para atualizar tabela")
                        .Property("Tabela", table)
                        .Property("Colunas", properties.Keys)
                        .Property("Valores", properties.Values)
                        .Property("Onde as colunas", whereProperties.Keys)
                        .Property("Correspondem a", whereProperties.Values)
                        .Log();
#endif

                    return false;
                }

            }
            catch (Exception ex)
            {
                GlobalFunctionalities.Logger.ForErrorEvent()
                    .Message("Problemas para atualizar tabela")
                        .Property("Tabela", table)
                        .Property("Colunas", properties.Keys)
                        .Property("Valores", properties.Values)
                        .Property("Onde as colunas", whereProperties.Keys)
                        .Property("Correspondem a", whereProperties.Values)
                        .Exception(ex)
                        .Log();

                return false;
            }
        }

        protected static List<Dictionary<string, object>> SelectAll(string table)
        {
            try
            {
                MySqlCommand? cmd;
                string sql = $"SELECT * FROM `{table}`;";
                List<Dictionary<string, object>> elements = new List<Dictionary<string, object>>();
                Dictionary<string, object> element = new Dictionary<string, object>();

                MySqlConnection conn = MySqlConn.OpenConn();
                
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        element.Add(rdr.GetName(i), rdr.GetValue(i));
                    }
                    elements.Add(element.ToDictionary());
                    element.Clear();
                }

                rdr.Close();
                conn.Close();
                

                if (element.Count > 0)
                    return elements;
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

        protected static List<Dictionary<string, object>> SelectWholeByProperties(string table, Dictionary<string, object> properties)
        {
            try
            {
                MySqlCommand? cmd;
                Dictionary<string, object> element = new Dictionary<string, object>();
                List<Dictionary<string, object>> elements = new List<Dictionary<string, object>>();

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

                MySqlConnection conn = MySqlConn.OpenConn();

                cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        element.Add(rdr.GetName(i), rdr.GetValue(i));
                    }

                    elements.Add(element.ToDictionary());
                    element.Clear();
                }

                rdr.Close();
                conn.Close();
                
#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Selecionando elemento/s")
                    .Property("Tabela", table)
                    .Property("Colunas de pesquisa", properties.Keys)
                    .Property("Valores de pesquisa", properties.Values)
                    .Property("QntRetorno", elements.Count)
                    .Property("Retorno", elements)
                    .Log();
#endif
                if (elements.Count > 0)
                    return elements;
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

        protected static List<Dictionary<string, object>> SelectSpecificsByProperties(string table, string[] specifics, Dictionary<string, object> properties)
        {
            try
            {
                MySqlCommand? cmd;
                List<Dictionary<string, object>> elements = new List<Dictionary<string, object>>();
                Dictionary<string, object> element = new Dictionary<string, object>();

                string selects = string.Empty;
                string where = string.Empty;
                byte counter;

                if (specifics.Length > 1)
                {
                    counter = 0;
                    foreach (string specific in specifics)
                    {
                        counter++;
                        if (counter < specifics.Length)
                        {
                            selects += $"{specific}, ";
                        }
                        else
                        {
                            selects += $"{specific}";
                        }

                    }
                }
                else
                {
                    selects += $"{specifics[0]}";
                }

                if (properties.Count > 1)
                {
                    counter = 0;
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

                string sql = $"SELECT {selects} FROM {table} WHERE {where};";

                MySqlConnection conn = MySqlConn.OpenConn();
                
                cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        element.Add(rdr.GetName(i), rdr.GetValue(i));
                    }
                    elements.Add(element.ToDictionary());
                    element.Clear();
                }

                rdr.Close();
                conn.Close();

#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Selecionando elemento/s")
                    .Property("Tabela", table)
                    .Property("Propriedades selecionadas", specifics)
                    .Property("Colunas de pesquisa", properties.Keys)
                    .Property("Valores de pesquisa", properties.Values)
                    .Property("QntRetorno", elements.Count)
                    .Property("Retorno", elements)
                    .Log();
#endif

                if (elements.Count > 0)
                    return elements;
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

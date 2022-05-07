using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Threading.Tasks;

static class MyDataBase
    {
        private const string fileName = "energy.db";
        private static string DBPath;
        private static SqliteConnection connection;
        private static SqliteCommand command;

        static MyDataBase()
        {
            DBPath = GetDatabasePath();

        }


        private static string GetDatabasePath()
        {
#if UNITY_EDITOR
            return Path.Combine(Application.streamingAssetsPath, fileName);
#if UNITY_STANDALONE
            string filePath = Path.Combine(Application.dataPath, fileName);
            if (!File.Exists(filePath)) UnpackDatabase(filePath);
            return filePath;
#endif
#endif
        }

        private static void UnpackDatabase(string toPath)
        {
            string fromPath = Path.Combine(Application.streamingAssetsPath, fileName);
            WWW reader = new WWW(fromPath);
            while (!reader.isDone) { }
            File.WriteAllBytes(toPath, reader.bytes);
        }
        // Подключение к БД
        private static async Task OpenConnection()
        {
            connection = new SqliteConnection("Data Source=" + DBPath);
            command = new SqliteCommand(connection);
            await connection.OpenAsync();
            //Debug.Log("!");
        }

        // Закрытие поключения
        public static async Task CloseConnection()
        {
            await connection.CloseAsync();
            command.Dispose();
        }

        //Метод для передачи запроса, возвращает строку
        public static async Task<string> ExecuteQueryWithAnswer(string query)
        {
            await OpenConnection();
            command.CommandText = query;
            var answer = await command.ExecuteScalarAsync();
            await CloseConnection();
            if (answer != null) return answer.ToString();
            else return null;
        }

        // Метод для передачи запроса, возвращает таблицу
        public static async Task<DataTable> GetTable(string query)
        {
            await OpenConnection();
            SqliteDataAdapter adapter = new SqliteDataAdapter(query, connection);
            DataSet DS = new DataSet();
            adapter.Fill(DS);
            adapter.Dispose();
            await CloseConnection();
            return DS.Tables[0];
        }
    }
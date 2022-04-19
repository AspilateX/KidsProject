using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

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
        private static void OpenConnection()
        {
            connection = new SqliteConnection("Data Source=" + DBPath);
            command = new SqliteCommand(connection);
            connection.Open();
            //Debug.Log("!");
        }

        // Закрытие поключения
        public static void CloseConnection()
        {
            connection.Close();
            command.Dispose();
        }

        //Метод для передачи запроса, возвращает строку
        public static string ExecuteQueryWithAnswer(string query)
        {
            OpenConnection();
            command.CommandText = query;
            var answer = command.ExecuteScalar();
            CloseConnection();
            if (answer != null) return answer.ToString();
            else return null;
        }

        // Метод для передачи запроса, возвращает таблицу
        public static DataTable GetTable(string query)
        {
            OpenConnection();
            SqliteDataAdapter adapter = new SqliteDataAdapter(query, connection);
            DataSet DS = new DataSet();
            adapter.Fill(DS);
            adapter.Dispose();
            CloseConnection();
            return DS.Tables[0];
        }
    }
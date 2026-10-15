using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace LabCSharp2
{
    internal class SQLManager
    {
        private static SQLiteConnection _conn = null;
        public static void connectToDatabase()
        {
            
            if (!File.Exists("KeyWords.db"))
            {
                 SQLiteConnection.CreateFile("KeyWords.db");
            }
            try
            {
                _conn = new SQLiteConnection("DataSource = KeyWords.db; Version = 3; ");
                _conn.Open();
                const string stmt =
                    "CREATE TABLE IF NOT EXISTS Keywords (" +
                    "    id   INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "    word TEXT NOT NULL UNIQUE" +
                    ");";

                SQLiteCommand command = new SQLiteCommand(stmt, _conn);

                command.ExecuteNonQuery();
            } catch (SQLiteException e)
            {
                Console.WriteLine("Connection failed");
                _conn = null;
            }
        }
        public static void addKeyword(string keyword)
        {
            if (_conn == null)
            {
                MessageBox.Show("First connect to the database!");
                return;
            }
            
            const string sql = "INSERT OR IGNORE INTO Keywords (word) VALUES (@word);";

            using (var command = new SQLiteCommand(sql, _conn))
            {
                command.Parameters.AddWithValue("@word", keyword);
                command.ExecuteNonQuery();
            }
        }
        public static List<string> showKeywords()
        {
            List<string> keywords = new List<string>();
            if (_conn == null)
            {
                MessageBox.Show("First connect to the database!");
                return null;
            }

            const string sql = "SELECT * FROM Keywords";

            using (var command = new SQLiteCommand(sql, _conn))
            {
                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        keywords.Add(reader.GetString(1));
                    }
                }
            }
            return keywords;
        }
        public static void Disconnect()
        {
            _conn.Dispose();
        }
    }
}

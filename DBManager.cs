using Webtech;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using static System.IO.File;
using System.Data;
using System.Text;

namespace Tourism {
    internal partial class DBManager : IDisposable {
        SqliteConnection db;
        SqliteCommand sql;
        public DBManager(string file) {
            db = new($"Data Source={file};Mode=ReadWrite;");
            db.Open();
            sql = new() {
                Connection = db
            };
        }
        public bool ExistUser(string login, string password) {
            sql.CommandText = $"SELECT EXISTS(SELECT id from Users WHERE login='{login}' AND password='{password}');";
            return (long)sql.ExecuteScalar()! == 1;
        }
        public string UserName(string login) {
            sql.CommandText = $"SELECT name FROM Users WHERE login='{login}';";
            return (string)sql.ExecuteScalar()!;
        }
        public bool AddUser(string login, string password, string name) {
            sql.CommandText = $"INSERT INTO Users (login, password, name) VALUES ('{login}', '{password}', '{name}');";
            return sql.ExecuteNonQuery() > 0;
        }
        public string GetPlaces() {
            var json = "[\n";
            List<string> data = new();
            sql.Dispose();
            sql.CommandText = "SELECT * FROM Places;";
            var reader = sql.ExecuteReader();
            while(reader.Read()) {
                string desc_buff = $"{ApiImpl.WorkDir}/resources/data/{reader["description"]}";
                if(Exists(desc_buff))
                    desc_buff = ReadAllText(desc_buff);
                else {
                    ApiImpl.logger.Error("DB", $"Error of reading place description from {desc_buff}");
                    desc_buff = "";
                }
                data.Add("\t{"
                + $"\n\t\t\"id\": {reader["id"]},\n" 
                + $"\n\t\t\"name\": \"{reader["name"]}\",\n" 
                + $"\n\t\t\"photo_url\": \"/resources/img/{reader["photo"]}\",\n"
                + $"\n\t\t\"description\": \"{desc_buff}\"\n"
                + "\t}");
            }

            return json + string.Join(", \n", data) + "\n]";
        }
        public string GetTours() {
            var json = "[\n";
            List<string> data = new();
            sql.Dispose();
            sql.CommandText = "SELECT * FROM Tours;";
            var reader = sql.ExecuteReader();
            while(reader.Read()) {
                data.Add("\t{"
                + $"\n\t\t\"id\": {reader["id"]},\n" 
                + $"\n\t\t\"title\": {reader["title"]},\n" 
                + $"\n\t\t\"places\": {reader["places"]}\n"
                + "\t}");
            }

            return json + string.Join(", \n", data) + "\n]";
        }
        public string GetComments(int tour_id) {
            var json = "[\n";
            List<string> data = new();
            sql.Dispose();
            sql.CommandText = $"SELECT * FROM Comments WHERE tour = {tour_id};";
            var reader = sql.ExecuteReader();
            while(reader.Read()) {
                data.Add("\t{"
                + $"\n\t\t\"id\": {reader["id"]},\n" 
                + $"\n\t\t\"name\": {reader["name"]},\n" 
                + $"\n\t\t\"tour_id\": {reader["tour"]}\n"
                + $"\n\t\t\"comment\": {reader["comment"]}\n"
                + "\t}");
            }
            return json + string.Join(", \n", data) + "\n]";
        }
        public bool AddComment(string username, int tour_id, string comment) {
            sql.CommandText = $"INSERT INTO Comments (name, tour, comment) VALUES ('{username}', '{tour_id}', '{comment}');";
            return sql.ExecuteNonQuery() > 0;
        }
        public void Dispose() {
            GC.SuppressFinalize(this);
            sql.Dispose();
            db.Close();
            db.Dispose();
        }

        ~DBManager() {
            this.Dispose();
        }
    }
}
using Webtech;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using System;

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
        public string UserId(string login) {
            sql.CommandText = $"SELECT id FROM Users WHERE login='{login}';";
            return sql.ExecuteReader()["id"].ToString()!;
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
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace IT
{
    public class DBHelper
    {
        static public string user = "root";
        static public string password = "toor";
        static public string server = "localhost";
        static public string db = "it";
        public static string DBFileName = "bd.db";
        static public string connStr = "datasource=" + DBFileName + ";Pooling=true;FailIfMissing=false;Version = 3";
        public SQLiteConnection conn;
        public string sql;
        public SQLiteCommand command;
        public SQLiteDataReader reader;

        public DBHelper()
        {
            conn = new SQLiteConnection(connStr);
            conn.Open();
        }

        public List<Xpert> GetXperts()
        {
            sql = "";
            command = new SQLiteCommand(sql, conn);
            SQLiteDataReader rd = command.ExecuteReader();
            List<Xpert> xperts = new List<Xpert>(); 
            if(rd.HasRows)
            {
                while(rd.Read())
                xperts.Add(new Xpert
                {
                    Id = (uint)rd["Id"],
                    Name = rd["Name"].ToString(),
                    Surname = rd["Surname"].ToString(),
                    Email = rd["Email"].ToString(),
                    NumPhone = rd["NumPhone"].ToString(),
                });
            }
            return xperts;
        }


        public List<Question> GetQuestion()
        {
            sql = "";
            command = new SQLiteCommand(sql, conn);
            SQLiteDataReader rd = command.ExecuteReader();
            List<Question> questions = new List<Question>();
            if (rd.HasRows)
            {
                while (rd.Read())
                    questions.Add(new Question
                    {
                        Id = (uint)rd["Id"],
                        Discription = rd["Discription"].ToString()
                    });
            }
            return questions;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace IT
{
    public class DBHelper
    {
        public static string DBFileName = "IT.db";
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

        public bool AddXpert(Xpert xpert)
        {
            sql = $"SELECT * FROM Xpert WHERE (Id = {xpert.Id}";
            command = new SQLiteCommand(sql, conn);
            var rd = command.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Close();
                sql = $"INSERT INTO xpert (`Name`, `Surname`, `Email`, `NumPhone`) " +
                    $" VALUES('{xpert.Name}', '{xpert.Surname}', '{xpert.Email}', '{xpert.NumPhone}');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                return true;
            }
            rd.Close();
            return false;
        }

        public List<KeyValuePair<uint, List<uint>>> GetCompetence()
        {
            List<KeyValuePair<uint, List<uint>>> result = new List<KeyValuePair<uint, List<uint>>>();
            sql = "SELECT * FROM Competence";
            command = new SQLiteCommand(sql, conn);
            SQLiteDataReader rd = command.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    var id = result.FindIndex(x => x.Key.ToString() == rd["id_xpert"].ToString());
                    if (id == -1)
                    {
                        result.Add(new KeyValuePair<uint, List<uint>>((uint)rd["id_xpert"], new List<uint> { (uint)rd["id_question"] }));
                    }
                    else
                    {
                        result[id].Value.Add((uint)rd["id_question"]);
                    }
                }
            }
            rd.Close();

            return result;

        }
 
        public List<Xpert> GetXperts()
        {
            sql = "SELECT * FROM Xpert";
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
            rd.Close();

            return xperts;
        }

        public List<Question> GetQuestion()
        {
            sql = "SELECT * FROM Question";
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
            rd.Close();
            return questions;
        }
    }
}

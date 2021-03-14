using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBarMemo.Models
{
    class MemoRepository : IMemoRepository
    {
        private SQLiteConnectionStringBuilder _SQLiteConnectionStringBuilder = new() { DataSource = "MemoDatas.db" };


        public void Dispose()
        {
            _SQLiteConnectionStringBuilder = null;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MemoRepository()
        {
            // データベースファイルが存在しなければ作成する
            if (System.IO.File.Exists("MemoDatas.db") == false)
            {
                CreateTable();
            }
        }

        /// <summary>
        /// メモリ上に展開する場合のみ
        /// </summary>
        public void CreateTable()
        {
            using (var cn = new SQLiteConnection(_SQLiteConnectionStringBuilder.ToString()))
            {
                cn.Open();

                using (var cmd = new SQLiteCommand(cn))
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS memo(" +
                        "Id TEXT NOT NULL PRIMARY KEY," +
                        "Body TEXT NOT NULL," +
                        "Time TEXT NOT NULL)";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<MemoData> GetMemos()
        {
            List<MemoData> Memos = new();
            using (var cn = new SQLiteConnection(_SQLiteConnectionStringBuilder.ToString()))
            {
                cn.Open();

                using (var cmd = new SQLiteCommand(cn))
                {
                    cmd.CommandText = "SELECT * FROM memo";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Memos.Add(new MemoData()
                            {
                                Guid = reader["Id"].ToString(),
                                MemoBody = reader["Body"].ToString(),
                                MemoTime = DateTime.Parse(reader["Time"].ToString())
                            });
                        }
                    }
                }
            }

            return Memos;
        }

        public void SaveMemo(MemoData memo)
        {
            using (var cn = new SQLiteConnection(_SQLiteConnectionStringBuilder.ToString()))
            {
                cn.Open();

                using (var cmd = new SQLiteCommand(cn))
                {
                    cmd.CommandText = $"INSERT INTO memo(Id, Body, Time) VALUES('{memo.Guid}', '{memo.MemoBody}', '{memo.MemoTime}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public static class SQLiteExtension
    {
        public static string DumpQuery(this SQLiteDataReader reader)
        {
            var i = 0;
            var sb = new StringBuilder();
            while (reader.Read())
            {
                if (i == 0)
                {
                    sb.AppendLine(string.Join("\t", reader.GetValues().AllKeys));
                    sb.AppendLine(new string('=', 8 * reader.FieldCount));
                }
                sb.AppendLine(string.Join("\t", Enumerable.Range(0, reader.FieldCount).Select(x => reader.GetValue(x))));
                i++;
            }

            return sb.ToString();
        }
    }
}

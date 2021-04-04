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
        /// 初回起動・データベースファイル損失時にテーブルを作成する
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
                        "Title TEXT," +
                        "Body TEXT NOT NULL," +
                        "Time TEXT NOT NULL)";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// メモを取得する
        /// </summary>
        /// <returns></returns>
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
                                MemoTitle = reader["Title"].ToString(),
                                MemoBody = reader["Body"].ToString(),
                                MemoTime = DateTime.Parse(reader["Time"].ToString())
                            });
                        }
                    }
                }
            }

            return Memos;
        }

        /// <summary>
        /// メモを保存する
        /// </summary>
        /// <param name="memo"></param>
        public void SaveMemo(MemoData memo)
        {
            using (var cn = new SQLiteConnection(_SQLiteConnectionStringBuilder.ToString()))
            {
                cn.Open();

                using (var cmd = new SQLiteCommand(cn))
                {
                    cmd.CommandText = $"INSERT INTO memo(Id, Title, Body, Time) VALUES('{memo.Guid}', '{memo.MemoTitle}', '{memo.MemoBody}', '{memo.MemoTime}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// メモを削除する
        /// </summary>
        /// <param name="memoId"></param>
        public void DeleteMemo(string memoId)
        {
            using (var cn = new SQLiteConnection(_SQLiteConnectionStringBuilder.ToString()))
            {
                cn.Open();

                using (var cmd = new SQLiteCommand(cn))
                {
                    cmd.CommandText = $"DELETE FROM memo WHERE Id LIKE '{memoId}'";
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

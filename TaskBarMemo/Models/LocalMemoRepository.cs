using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskBarMemo.Models
{
    class LocalMemoRepository : IMemoRepository
    {
        /// <summary>
        /// メモリ上に展開される一時的なリポジトリ
        /// </summary>
        private List<MemoData> LocalMemoDatas = new();

        /// <summary>
        /// メモを削除する
        /// </summary>
        /// <param name="memoId"></param>
        public void DeleteMemo(string memoId)
        {
            LocalMemoDatas.RemoveAll(x => x.Guid == memoId);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            LocalMemoDatas.Clear();
            LocalMemoDatas = null;
        }

        /// <summary>
        /// メモを取得する
        /// </summary>
        /// <returns></returns>
        public List<MemoData> GetMemos()
        {
            return LocalMemoDatas;
        }

        /// <summary>
        /// メモを保存する
        /// </summary>
        /// <param name="memo"></param>
        public void SaveMemo(MemoData memo)
        {
            LocalMemoDatas.Add(memo);
        }
    }
}

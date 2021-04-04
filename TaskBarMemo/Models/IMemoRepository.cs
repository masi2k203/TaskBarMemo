using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBarMemo.Models
{
    /// <summary>
    /// メモデータリポジトリのインターフェイス
    /// </summary>
    interface IMemoRepository : IDisposable
    {
        public List<MemoData> GetMemos();

        public void SaveMemo(MemoData memo);

        public void DeleteMemo(string memoId);
    }
}

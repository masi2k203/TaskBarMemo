using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBarMemo.Models
{
    class MemoDataAgent : IDisposable
    {
        /// <summary>
        /// メモデータのリポジトリ
        /// </summary>
        private IMemoRepository repository = null;

        /// <summary>
        /// メモ件数
        /// </summary>
        public int MemoCount { get; set; }

        /// <summary>
        /// メモを取得する
        /// </summary>
        /// <param name="memoId">取得したいメモのID</param>
        /// <returns>IDに対応したメモ</returns>
        public MemoData GetMemo(string memoGUID)
        {
            var memos = this.repository.GetMemos();

            return memos.Find(m => m.Guid == memoGUID);
        }

        /// <summary>
        /// メモを全件取得する
        /// </summary>
        /// <returns></returns>
        public List<MemoData> GetMemos()
        {
            return this.repository.GetMemos();
        }

        /// <summary>
        /// メモを保存する
        /// </summary>
        /// <param name="memo"></param>
        public void SaveMemo(MemoData memo)
        {
            repository.SaveMemo(memo);
        }

        public void DeleteMemo(string memoId)
        {
            repository.DeleteMemo(memoId);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MemoDataAgent(IMemoRepository memoRepository)
        {
            this.repository = memoRepository;
            MemoCount = memoRepository.GetMemos().Count;
        }

        public int GetMemoCount()
        {
            return repository.GetMemos().Count;
        }

        public void Dispose()
        {
            // テスト 特になし
        }
    }
}

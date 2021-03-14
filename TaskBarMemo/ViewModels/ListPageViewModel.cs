using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Reactive.Bindings;

namespace TaskBarMemo.ViewModels
{
    class ListPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// データエージェント
        /// </summary>
        private Models.MemoDataAgent DataAgent = null;

        /// <summary>
        /// メモデータのコレクション
        /// </summary>
        public ReactiveCollection<Models.MemoData> MemoCollection { get; set; } = new();

        #region コマンド

        /// <summary>
        /// リストビューアイテムの削除コマンド
        /// </summary>
        public ReactiveCommand ListViewItemDeleteCommand { get; set; } = new();

        #endregion


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ListPageViewModel()
        {
            // データエージェントの作成
            DataAgent = new Models.MemoDataAgent(new Models.MemoRepository());

            // メモ取得およびコレクションに追加
            foreach (var item in DataAgent.GetMemos())
            {
                MemoCollection.Add(item);
            }
            // コマンドの購読
            ListViewItemDeleteCommand.Subscribe(x =>
            {
                var selectMemo = (Models.MemoData)x;
                System.Diagnostics.Debug.WriteLine($"Selected : {selectMemo.Id} / Text : {selectMemo.MemoBody}");
            });
        }
    }
}

using System;
using System.Linq;
using System.ComponentModel;
using Reactive.Bindings;
using System.Reactive.Linq;
using System.ComponentModel.DataAnnotations;

namespace TaskBarMemo.ViewModels
{
    class HomePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// データエージェント
        /// </summary>
        private Models.MemoDataAgent DataAgent = null;

        #region プロパティ

        /// <summary>
        /// メモ本文テキストのプロパティ
        /// </summary>
        [Required(ErrorMessage = "必須項目")]
        public ReactiveProperty<string> MemoBodyProperty { get; set; } = new("メモを入力");

        #endregion

        #region Command

        /// <summary>
        /// [保存]ボタンにバインドするコマンド
        /// </summary>
        public ReactiveCommand SaveMemoCommand { get; set; } = new();

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HomePageViewModel()
        {
            DataAgent = new Models.MemoDataAgent(new Models.MemoRepository());

            // バリデーションチェック追加
            this.MemoBodyProperty.SetValidateAttribute(() => this.MemoBodyProperty);

            // メモを保存処理の購読
            SaveMemoCommand = MemoBodyProperty.ObserveHasErrors.Select(x => !x).ToReactiveCommand();
            SaveMemoCommand.Subscribe(_ => DataAgent.SaveMemo(new Models.MemoData()
            {
                Guid = Guid.NewGuid().ToString("N"),
                MemoBody = MemoBodyProperty.Value,
                MemoTime = DateTime.Now
            }));
        }


    }
}

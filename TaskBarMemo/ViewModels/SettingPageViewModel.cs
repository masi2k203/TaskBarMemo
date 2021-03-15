using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Reactive.Bindings;
using System.Diagnostics;
using System.Reflection;

namespace TaskBarMemo.ViewModels
{
    class SettingPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region プロパティ

        /// <summary>
        /// アセンブリファイルバージョンのプロパティ
        /// </summary>
        public ReactiveProperty<string> FileVersionProperty { get; set; } = new();
        /// <summary>
        /// 追加のバージョンプロパティ（非常用・Devチャネルのみ）
        /// </summary>
        public ReactiveProperty<string> AdditionalVersionProperty { get; set; } = new("_alpha1_release_dev");

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SettingPageViewModel()
        {
            // アセンブリファイルバージョンを取得する
            var assembly = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            FileVersionProperty.Value = assembly.FileVersion + AdditionalVersionProperty.Value;
        }


        #region メソッド

        #endregion
    }
}

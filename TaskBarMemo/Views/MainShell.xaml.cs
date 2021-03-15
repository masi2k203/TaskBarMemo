using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaskBarMemo.Views
{
    public enum NaviIcon
    {
        Home,
        List,
        Setting,
        None
    }
    /// <summary>
    /// MainShell.xaml の相互作用ロジック
    /// ナビゲーションなど面倒な処理
    /// </summary>
    public partial class MainShell : Window
    {
        private static IReadOnlyDictionary<NaviIcon, Type> _pages = new Dictionary<NaviIcon, Type>()
        {
            {NaviIcon.Home, typeof(Views.HomePageView) },
            {NaviIcon.List, typeof(Views.ListPageView) },
            {NaviIcon.Setting, typeof(Views.SettingPageView) }
        };

        public MainShell()
        {
            InitializeComponent();
        }

        private void NavigationView_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            try
            {
                var selectedItem = (ModernWpf.Controls.NavigationViewItem)args.SelectedItem;
                string iconName = selectedItem.Tag?.ToString();

                if (Enum.TryParse(iconName, out NaviIcon icon))
                {
                    ContentFrame.Navigate(_pages[icon]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

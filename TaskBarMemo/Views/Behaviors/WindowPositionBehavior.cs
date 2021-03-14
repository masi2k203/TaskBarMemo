using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace TaskBarMemo.Views.Behaviors
{
    public class WindowPositionBehavior : Behavior<Window>
    {
        public int Top
        {
            get { return (int)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof(int), typeof(WindowPositionBehavior), new UIPropertyMetadata(null));

        public int Left
        {
            get { return (int)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(int), typeof(WindowPositionBehavior), new UIPropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Loaded += SetPosition;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Loaded -= SetPosition;
            base.OnDetaching();
        }

        private void SetPosition(object sender, RoutedEventArgs e)
        {
            var window = (Window)sender;
            window.WindowStartupLocation = WindowStartupLocation.Manual;
            window.Top = SystemParameters.WorkArea.Height - Top;
            window.Left = SystemParameters.WorkArea.Width - Left;
        }
    }
}

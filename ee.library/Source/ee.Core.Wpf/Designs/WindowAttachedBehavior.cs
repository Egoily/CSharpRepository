using System.Windows;

namespace ee.Core.Wpf.Designs
{
    public static class WindowAttachedBehavior
    {
        public static DependencyProperty IsCloseViewProperty =
            DependencyProperty.RegisterAttached("IsCloseView", typeof(bool),
            typeof(WindowAttachedBehavior), new UIPropertyMetadata(false, OnIsCloseView));


        public static bool GetIsCloseView(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCloseViewProperty);
        }

        public static void SetIsCloseView(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCloseViewProperty, value);
        }

        public static void OnIsCloseView(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            Window wnd = (Window)d;
            if ((bool)e.NewValue)
            {
                wnd?.Close();
            }
        }



        public static DependencyProperty IsHideViewProperty =
     DependencyProperty.RegisterAttached("IsHideView", typeof(bool),
     typeof(WindowAttachedBehavior), new UIPropertyMetadata(false, OnIsHideView));


        public static bool GetIsHideView(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsHideViewProperty);
        }

        public static void SetIsHideView(DependencyObject obj, bool value)
        {
            obj.SetValue(IsHideViewProperty, value);
        }

        public static void OnIsHideView(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            Window wnd = (Window)d;
            if ((bool)e.NewValue)
            {
                wnd?.Hide();
            }
        }
    }
}

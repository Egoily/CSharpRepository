using System.Windows;
using System.Windows.Controls;

namespace TaobaoSeckiller.Controls
{
    public class TextImage : Control
    {
        static TextImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextImage), new FrameworkPropertyMetadata(typeof(TextImage)));
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(FrameworkElement));
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(string), typeof(FrameworkElement));

        public string Text
        {
            set
            {
                SetValue(TextProperty, value);
            }
            get
            {
                return (string)GetValue(TextProperty);
            }
        }

        public string Image
        {
            set
            {
                SetValue(ImageProperty, value);
            }
            get
            {
                return (string)GetValue(ImageProperty);
            }
        }
    }
}

﻿using ee.Core.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ee.Core.Wpf.Converters
{


    /// <summary>
    /// This converter does nothing except breaking the
    /// debugger into the convert method
    /// </summary>
    public class DatabindingDebugConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            System.Diagnostics.Debugger.Break();
            return value;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            System.Diagnostics.Debugger.Break();
            return value;
        }
    }

    public class EnumToBooleanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? false : value.Equals(parameter);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }


    public class VisibilityToBool : IValueConverter
    {
        public bool Inverted { get; set; } = false;

        public bool Not { get; set; } = false;

        private object VisToBool(object value)
        {
            if (!(value is Visibility))
            {
                return DependencyProperty.UnsetValue;
            }

            return (((Visibility)value) == Visibility.Visible) ^ Not;
        }

        private object BoolToVis(object value)
        {
            if (!(value is bool))
            {
                return DependencyProperty.UnsetValue;
            }

            return ((bool)value ^ Not) ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType,
                object parameter, CultureInfo culture)
        {
            return Inverted ? BoolToVis(value)
                : VisToBool(value);
        }

        public object ConvertBack(object value, Type targetType,
                object parameter, CultureInfo culture)
        {
            return Inverted ? VisToBool(value)
                : BoolToVis(value);
        }
    }

    public class VisibleToReverse : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }


    }


    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? bValue = value as bool?;

            if (bValue.HasValue)
            {
                return bValue.Value ? Visibility.Visible : Visibility.Collapsed;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility? visibility = value as Visibility?;

            if (visibility.HasValue)
            {
                return (visibility.Value == Visibility.Visible) ? true : false;
            }

            return DependencyProperty.UnsetValue;
        }

        #endregion
    }

    public class BoolToReverseVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? bValue = value as bool?;

            if (bValue.HasValue)
            {
                return bValue.Value ? Visibility.Collapsed : Visibility.Visible;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility? visibility = value as Visibility?;

            if (visibility.HasValue)
            {
                return (visibility.Value == Visibility.Collapsed) ? true : false;
            }

            return DependencyProperty.UnsetValue;
        }

        #endregion
    }

    public class ImageSourceConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Bitmap)
            {
                return ImageHelper.ToImageSource(value as Bitmap);
            }
            else if (value is string)
            {
                return ImageHelper.ToImageSourceFromFile(value as string);
            }
            else
            {
                return null;
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
    public class FirstCharConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString().Substring(0, 1)?.ToUpper() ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;

        }
    }
    public class NullableToVisibilityConverter : IValueConverter
    {
        public Visibility NullValue { get; set; } = Visibility.Visible;
        public Visibility NotNullValue { get; set; } = Visibility.Collapsed;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? NullValue : NotNullValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
    public class NullableToReverseVisibilityConverter : IValueConverter
    {
        public Visibility NullValue { get; set; } = Visibility.Collapsed;
        public Visibility NotNullValue { get; set; } = Visibility.Visible;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? NullValue : NotNullValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }


    public class VisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return Visibility.Visible;
            }

            if (value is string)
            {
                if ((string)value == "Visible")
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (value is IList<string>)
            {
                if ((value as IList<string>).Contains(parameter.ToString()))
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (value is string[])
            {

                if ((value as string[]).ToList().Contains(parameter.ToString()))
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (value is ObservableCollection<string>)
            {

                if ((value as ObservableCollection<string>).Contains(parameter.ToString()))
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }

            else
            {
                return Visibility.Visible;
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }


}

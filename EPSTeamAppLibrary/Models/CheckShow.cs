using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EPSTeamAppLibrary.Models
{
    public sealed class BooleanToHiddenVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = false;
            if (value is bool)
            {
                bValue = (bool)value;
            }
            return (bValue) ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
            {
                return (Visibility)value == Visibility.Visible;
            }
            return false;
        }
    }
}

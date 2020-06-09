using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MaSchoeller.Dublin.Client.Helpers
{
    public class BooleanToHiddenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                true => Visibility.Visible,
                false => Visibility.Collapsed,
                _ => throw new InvalidCastException($"{nameof(value)} must be from type {typeof(bool).FullName}.")
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
            => throw new NotSupportedException();
    }
}

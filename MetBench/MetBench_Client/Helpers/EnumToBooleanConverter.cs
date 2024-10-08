﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace  MetBench_Client.Helpers
{
    internal class EnumToBooleanConverter : IValueConverter
    {
        public EnumToBooleanConverter()
        {
        }

        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (parameter is not String enumString)
        //        throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");

        //    if (!Enum.IsDefined(typeof(Wpf.Ui.Appearance.ThemeType), value))
        //        throw new ArgumentException("ExceptionEnumToBooleanConverterValueMustBeAnEnum");

        //    var enumValue = Enum.Parse(typeof(Wpf.Ui.Appearance.ThemeType), enumString);

        //    return enumValue.Equals(value);
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (parameter is not String enumString)
        //        throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");

        //    return Enum.Parse(typeof(Wpf.Ui.Appearance.ThemeType), enumString);
        //}
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not string enumString)
            {
                throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");
            }

            if (!Enum.IsDefined(typeof(Wpf.Ui.Appearance.ApplicationTheme), value))
            {
                throw new ArgumentException("ExceptionEnumToBooleanConverterValueMustBeAnEnum");
            }

            var enumValue = Enum.Parse(typeof(Wpf.Ui.Appearance.ApplicationTheme), enumString);

            return enumValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not string enumString)
            {
                throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");
            }

            return Enum.Parse(typeof(Wpf.Ui.Appearance.ApplicationTheme), enumString);
        }
    }
}

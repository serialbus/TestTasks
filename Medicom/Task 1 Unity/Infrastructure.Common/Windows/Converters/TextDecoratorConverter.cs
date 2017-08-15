using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Infrastructure.Common.Windows.Converters
{
    public class TextDecoratorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = value as string;

            if (String.IsNullOrEmpty(input))
                return null;

            var words = input.Split(' ');
            var textBlock = new TextBlock();
            textBlock.TextWrapping = TextWrapping.NoWrap;

            for (int i = 0; i < words.Length; i++)
            { 
                if (Regex.IsMatch(words[i].Trim(), @"<strong>(.*)</strong>"))
                {
                    var strong = Regex.Match(words[i], @"(?<=<strong>)(.*)(?=</strong>)");
                    textBlock.Inlines.Add(new Run(strong.Value) { Background = Brushes.Red });
                }
                else
                {
                    textBlock.Inlines.Add(new Run(words[i]));
                }

                if (i < words.Length - 1)
                    textBlock.Inlines.Add(new Run(" "));
            }
            return textBlock;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

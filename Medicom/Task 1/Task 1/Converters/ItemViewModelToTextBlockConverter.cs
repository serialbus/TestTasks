using Medicom.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Medicom.Converters
{
    public class ItemViewModelToTextBlockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var textBlock = new TextBlock();

            if (value is ItemViewModel)
            {
                var vm = value as ItemViewModel;

                textBlock.Inlines.Add(vm.IsHighLightedName ? new Run(vm.Name) { Background = Brushes.Red } : new Run(vm.Name));
                textBlock.Inlines.Add(" ");

                if (vm.IsCreditCard)
                {
                    textBlock.Inlines.Add(vm.IsHighLighteNumber ? new Run(vm.Number.ToString()) { Background = Brushes.Red } : new Run(vm.Number.ToString()));
                    textBlock.Inlines.Add(" ");
                    textBlock.Inlines.Add(vm.IsHighLightedExpirationDate ? new Run(vm.ExpirationDate.ToShortDateString()) { Background = Brushes.Red } : 
                        new Run(vm.ExpirationDate.ToShortDateString()));
                }
                else if (vm.IsNote)
                {
                    textBlock.Inlines.Add(vm.IsHighLightedContent ? new Run(vm.Content) { Background = Brushes.Red } : new Run(vm.Content));
                }
                else if (vm.IsWebAccount)
                {
                    textBlock.Inlines.Add(vm.IsHighLightedUrl ? new Run(vm.Url) { Background = Brushes.Red } : new Run(vm.Url));
                }
            }

            return textBlock;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

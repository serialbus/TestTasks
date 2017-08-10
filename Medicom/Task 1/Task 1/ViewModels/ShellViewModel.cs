using Prism.Mvvm;
using System.ComponentModel.Composition;

namespace Task.ViewModels
{
    [Export("ShellViewModel", typeof(ShellViewModel))]
    public class ShellViewModel : BindableBase
    {
        private string _title = "Тестовое задание 1";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ShellViewModel()
        {
        }
    }
}

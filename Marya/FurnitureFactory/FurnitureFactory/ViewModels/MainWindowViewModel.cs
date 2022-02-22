using Prism.Mvvm;

namespace FurnitureFactory.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Мебельная фабрика";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}

using Infrastructure.Common.Models.DAL;
using Infrastructure.Common.Services.Configuration;
using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;

namespace Task.ViewModels
{
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel : BindableBase
    {
        #region Constructors

        //[ImportingConstructor()]
        public ShellViewModel()
        {
            _ExitCommand = new DelegateCommand(OnExit);
        }

        [ImportingConstructor()]
        public ShellViewModel(
            [Import(typeof(IConfigurationService), RequiredCreationPolicy = CreationPolicy.Shared)]
            IConfigurationService configurationService)//, IRepositoryService repositoryService)
        {
            ConfigurationService = configurationService;
        //    //RepositoryService = repositoryService;
        //    _ExitCommand = new DelegateCommand(OnExit);
        }

        #endregion

        #region Fields And Properties

        [Import(typeof(IConfigurationService), RequiredCreationPolicy = CreationPolicy.Shared)]
        public IConfigurationService ConfigurationService { get; set; }
        [Import(typeof(IRepositoryService), RequiredCreationPolicy = CreationPolicy.Shared)]
        public IRepositoryService RepositoryService { get; set; }

        private string _title = "Тестовое задание 1";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion

        #region Commands
        public ICommand ExitCommand { get { return _ExitCommand; } }
        private readonly DelegateCommand _ExitCommand;
        private void OnExit()
        {
            var x = ConfigurationService;
            Application.Current.Shutdown();
        }

        #endregion
    }
}

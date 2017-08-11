using Infrastructure.Common.Events.DAL;
using Infrastructure.Common.Models.Configuration;
using Infrastructure.Common.Models.DAL;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Windows;
using System.Windows.Input;

namespace Medicom.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        #region Consturctors

        public ShellViewModel(IUnityContainer container)
        {
            _ExitCommand = new DelegateCommand(OnExit);
            _SaveFileCommand = new DelegateCommand(OnSaveFile, CanSaveFile);

            ConfigurationService = container.Resolve<IConfigurationService>();
            RepositoryService = container.Resolve<IRepositoryService>();
            container.Resolve<IEventAggregator>().GetEvent<DbConnectionStatusChangedEvent>().Subscribe(OnConnectionStatusChanged, ThreadOption.UIThread);

            OnConnectionStatusChanged();

            RepositoryService.Open();
        }

        #endregion

        #region Fields And Properties

        private readonly IConfigurationService ConfigurationService;
        private readonly IRepositoryService RepositoryService;

        private string _title = "Задание 1";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool IsSaveFile
        {
            get { return _SaveFileCommand.CanExecute(); }
        }
        public bool IsSaveASFile
        {
            get { return _SaveFileCommand.CanExecute(); }
        }
        #endregion

        #region Methods

        private void OnConnectionStatusChanged()
        {
            _SaveFileCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged("IsSaveFile");
        }

        #endregion

        #region Commands

        public ICommand ExitCommand { get { return _ExitCommand; } }
        private readonly DelegateCommand _ExitCommand;
        private void OnExit()
        {
            Application.Current.Shutdown();
        }

        public ICommand SaveFileCommand { get { return _SaveFileCommand; } }
        private readonly DelegateCommand _SaveFileCommand;
        private void OnSaveFile()
        {
            RepositoryService.SaveChanges();
        }
        private bool CanSaveFile()
        {
            return RepositoryService.IsConnect;
        }


        #endregion
    }
}

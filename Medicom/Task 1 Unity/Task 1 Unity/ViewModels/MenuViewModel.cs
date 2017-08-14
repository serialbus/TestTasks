using DAL.Services;
using Infrastructure.Common.Events.DAL;
using Infrastructure.Common.Models.DAL;
using Infrastructure.Common.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Medicom.ViewModels
{
	public class MenuViewModel : BindableBase
    {
        #region Constructors

        public MenuViewModel(IUnityContainer container)
        {
            _ExitCommand = new DelegateCommand(OnExit);
            _SaveFileCommand = new DelegateCommand(OnSaveFile, CanSaveFile);

            ApplicationCommandsService = container.Resolve<IApplicationCommandsService>();
            ApplicationCommandsService.AddNoteCommand.CanExecuteChanged += 
                (object sender, EventArgs e) => { RaisePropertyChanged("IsEnableAddNote"); };
            ApplicationCommandsService.AddWebAccountCommand.CanExecuteChanged +=
                (object sender, EventArgs e) => { RaisePropertyChanged("IsEnabledAddWebAccount"); };
            ApplicationCommandsService.AddCreditCardCommand.CanExecuteChanged +=
                (object sender, EventArgs e) => { RaisePropertyChanged("IsEnabledAddCreditCard"); };

            RepositoryService = container.Resolve<IRepositoryService>();
            container.Resolve<IEventAggregator>().GetEvent<DbConnectionStatusChangedEvent>().Subscribe(OnConnectionStatusChanged, ThreadOption.UIThread);

            OnConnectionStatusChanged();

            RepositoryService.Open();
        }

        #endregion

        #region Fields And Properties

        private readonly IRepositoryService RepositoryService;
        private readonly IApplicationCommandsService ApplicationCommandsService;

        public bool IsEnableSaveFile
        {
            get { return _SaveFileCommand.CanExecute(); }
        }
        public bool IsEnableAddNote
        {
            get { return AddNoteCommand == null ? false : AddNoteCommand.CanExecute(null); }
        }
        public bool IsEnabledAddWebAccount
        {
            get { return AddWebAccountCommand == null ? false : AddWebAccountCommand.CanExecute(null); }
        }
        public bool IsEnabledAddCreditCard
        {
            get { return AddCreditCardCommand == null ? false : AddCreditCardCommand.CanExecute(null); }
        }

        #endregion

        #region Methods

        private void OnConnectionStatusChanged()
        {
            _SaveFileCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged("IsEnableSaveFile");
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

        public ICommand AddNoteCommand
        {
            get { return ApplicationCommandsService.AddNoteCommand; }
        }
        
        public ICommand AddWebAccountCommand
        {
            get { return ApplicationCommandsService.AddWebAccountCommand; }
        }

        public ICommand AddCreditCardCommand
        {
            get { return ApplicationCommandsService.AddCreditCardCommand; }
        }

        #endregion
    }
}

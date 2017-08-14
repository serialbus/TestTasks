using Infrastructure.Common.Events.DAL;
using Infrastructure.Common.Models;
using Infrastructure.Common.Models.DAL;
using Infrastructure.Common.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Medicom.ViewModels
{
    public class ItemsViewModel : BindableBase
    {
        #region Consturctors

        public ItemsViewModel(IUnityContainer container)
        {
            _AddNoteCommand = new DelegateCommand(OnAddNote, CanAddNote);
            _AddWebAccountCommand = new DelegateCommand(OnAddWebAccount, CanAddWebAccount);
            _AddCreditCardCommand = new DelegateCommand(OnAddCreditCard, CanAddCreditCard);
            _DeleteItemCommand = new DelegateCommand(OnDeleteItem, CanDeleteItem).ObservesProperty(() => SelectedItem);

            RepositoryService = container.Resolve<IRepositoryService>();
            Items = RepositoryService.IsConnect ? 
                new ObservableCollection<ItemViewModel>(RepositoryService.Items.Select(x => new ItemViewModel(x))) : 
                new ObservableCollection<ItemViewModel>();
            SelectedItem = Items.FirstOrDefault();

            var appCmdService = container.Resolve<IApplicationCommandsService>();
            appCmdService.AddNoteCommand.RegisterCommand(AddNoteCommand);
            appCmdService.AddWebAccountCommand.RegisterCommand(AddWebAccountCommand);
            appCmdService.AddCreditCardCommand.RegisterCommand(AddCreditCardCommand);
            appCmdService.DeleteItemCommand.RegisterCommand(DeleteItemCommand);

            container.Resolve<IEventAggregator>().GetEvent<DbConnectionStatusChangedEvent>().Subscribe(OnConnectionStatusChanged, ThreadOption.UIThread);
        }

        #endregion

        #region Fields And Properties

        private readonly IRepositoryService RepositoryService;

        public ObservableCollection<ItemViewModel> Items { get; private set; }

        private ItemViewModel _SelectedItem;
        public ItemViewModel SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                RaisePropertyChanged("SelectedItem");
                RaisePropertyChanged("IsVisibleItemDetails");
            }
        }

        public bool IsVisibleItemDetails
        {
            get { return _SelectedItem != null; }
        }

        #endregion

        #region Methods

        private void OnConnectionStatusChanged()
        {
            if (RepositoryService.IsConnect)
            {
                Items.Clear();
                Items.AddRange(RepositoryService.Items.Select(x=> new ItemViewModel(x)));
                SelectedItem = Items.FirstOrDefault();
            }
            else
            {
                Items.Clear();
                SelectedItem = null;
            }

            _AddNoteCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged("IsVisibleItemDetails");
            RaisePropertyChanged("IsCanAddItem");
        }

        #endregion

        #region Commands

        public ICommand AddNoteCommand { get { return _AddNoteCommand; } }
        private DelegateCommand _AddNoteCommand;
        private void OnAddNote()
        {
            var note = new ItemViewModel(new Note { Content = "Content", Name = "New Note" });
            Items.Add(note);
            SelectedItem = note;
            RepositoryService.Items.Add(note.Item);
        }
        private bool CanAddNote()
        {
            return RepositoryService.IsConnect;
        }

        public ICommand AddWebAccountCommand { get { return _AddWebAccountCommand; } }
        private DelegateCommand _AddWebAccountCommand;
        private void OnAddWebAccount()
        {
            var webAccount = new ItemViewModel(new WebAccount { Url = new UriBuilder().Uri, Name = "New WebAccount" });
            Items.Add(webAccount);
            SelectedItem = webAccount;
            RepositoryService.Items.Add(webAccount.Item);
        }
        private bool CanAddWebAccount()
        {
            return RepositoryService.IsConnect;
        }

        public ICommand AddCreditCardCommand { get { return _AddCreditCardCommand; } }
        private DelegateCommand _AddCreditCardCommand;
        private void OnAddCreditCard()
        {
            var creditCard = new ItemViewModel(new CreditCard { ExpirationDate = DateTime.Now, Name = "New Credit card" });
            Items.Add(creditCard);
            SelectedItem = creditCard;
            RepositoryService.Items.Add(creditCard.Item);
        }
        private bool CanAddCreditCard()
        {
            return RepositoryService.IsConnect;
        }

        public ICommand DeleteItemCommand { get { return _DeleteItemCommand; } }
        private DelegateCommand _DeleteItemCommand;
        private void OnDeleteItem()
        {
            RepositoryService.Items.Remove(SelectedItem.Item);
            Items.Remove(SelectedItem);
            SelectedItem = Items.FirstOrDefault();
        }
        private bool CanDeleteItem()
        {
            return RepositoryService.IsConnect && SelectedItem != null;
        }

        #endregion
    }
}

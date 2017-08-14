using Infrastructure.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medicom.ViewModels
{
    public class ItemViewModel : BindableBase
    {
        #region Constructors

        public ItemViewModel(ModelBase item)
        {
            Item = item;
        }

        #endregion

        #region Fields And Properties

        public ModelBase Item { get; private set; }

        public string ItemTypeName
        {
            get
            {
                if (IsNote)
                    return "Заметка";
                else if (IsCreditCard)
                    return "Кредитная карта";
                else if (IsWebAccount)
                    return "Web учётная запись";
                else
                    return string.Empty;
            }
        }

        public bool IsNote
        {
            get { return Item is Note; }
        }

        public bool IsWebAccount
        {
            get { return Item is WebAccount; }
        }

        public bool IsCreditCard
        {
            get { return Item is CreditCard; }
        }

        public string Name
        {
            get { return Item.Name; }
            set
            {
                Item.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Content
        {
            get { return Item is Note ? (Item as Note).Content : string.Empty; }
            set
            {
                if (Item is Note)
                {
                    var item = Item as Note;
                    item.Content = value;
                    RaisePropertyChanged("Content");
                }
            }
        }

        public string Url
        {
            get { return Item is WebAccount ? (Item as WebAccount).UrlAsString : string.Empty; }
            set
            {
                if (Item is WebAccount)
                {
                    var item = Item as WebAccount;
                    item.UrlAsString = value;
                    RaisePropertyChanged("Url");
                }
            }
        }

        public DateTime ExpirationDate
        {
            get { return Item is CreditCard ? (Item as CreditCard).ExpirationDate : DateTime.Now; }
            set
            {
                if (Item is CreditCard)
                {
                    var item = Item as CreditCard;
                    item.ExpirationDate = value;
                    RaisePropertyChanged("ExpirationDate");
                }
            }
        }

        public int Number
        {
            get { return Item is CreditCard ? (Item as CreditCard).Number : 0; }
            set
            {
                if (Item is CreditCard)
                {
                    var item = Item as CreditCard;
                    item.Number = value;
                    RaisePropertyChanged("Number");
                }
            }
        }

        public string Description
        {
            get { return Item.AsString(); }
        }

        #endregion
    }
}

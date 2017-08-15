using Infrastructure.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                RaisePropertyChanged("FormattedDescription");
            }
        }

        private bool _IsHighLightedName = false;
        public bool IsHighLightedName
        {
            get { return _IsHighLightedName; }
            set
            {
                _IsHighLightedName = value;
                RaisePropertyChanged("FormattedDescription");
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
                    RaisePropertyChanged("FormattedDescription");
                }
            }
        }

        private bool _IsHighLightedContent = false;
        public bool IsHighLightedContent
        {
            get { return _IsHighLightedContent; }
            set
            {
                _IsHighLightedContent = value;
                RaisePropertyChanged("FormattedDescription");
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
                    RaisePropertyChanged("FormattedDescription");
                }
            }
        }

        private bool _IsHighLightedUrl = false;
        public bool IsHighLightedUrl
        {
            get { return _IsHighLightedUrl; }
            set
            {
                _IsHighLightedUrl = value;
                RaisePropertyChanged("FormattedDescription");
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
                    RaisePropertyChanged("FormattedDescription");
                }
            }
        }

        private bool _IsHighLightedExpirationDate = false;
        public bool IsHighLightedExpirationDate
        {
            get { return _IsHighLightedExpirationDate; }
            set
            {
                _IsHighLightedExpirationDate = value;
                RaisePropertyChanged("FormattedDescription");
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
                    RaisePropertyChanged("FormattedDescription");
                }
            }
        }

        private bool _IsHighLighteNumber = false;
        public bool IsHighLighteNumber
        {
            get { return _IsHighLighteNumber; }
            set
            {
                _IsHighLighteNumber = value;
                RaisePropertyChanged("FormattedDescription");
            }
        }

        public string Description
        {
            get
            {
                if (IsNote)
                {
                    return String.Format("{0} {1}", Name, Content);
                }
                else if (IsCreditCard)
                {
                    return string.Format("{0} {1} {2}",
                        Name, Number, ExpirationDate.ToShortDateString());
                }
                else if (IsWebAccount)
                {
                    return String.Format("{0} {1}", Name, Url);
                }
                else
                    return String.Empty;
            }
        }

        public string FormattedDescription
        {
            get
            {
                if (IsNote)
                {
                    if (IsHighLightedName || IsHighLightedContent)
                    {
                        var sb = new StringBuilder();
                        if (IsHighLightedName)
                            sb.AppendFormat("<strong>{0}</strong> ", Name);
                        else
                            sb.Append(Name + " ");

                        if (IsHighLightedContent)
                            sb.AppendFormat("<strong>{0}</strong>", Content);
                        else
                            sb.Append(Content);
                        return sb.ToString();
                    }
                }
                else if (IsCreditCard)
                {
                    if (IsHighLightedName || IsHighLighteNumber || IsHighLightedExpirationDate)
                    {
                        var sb = new StringBuilder();

                        if (IsHighLightedName)
                            sb.AppendFormat("<strong>{0}</strong> ", Name);
                        else
                            sb.Append(Name + " ");

                        if (IsHighLighteNumber)
                            sb.AppendFormat("<strong>{0}</strong> ", Number);
                        else
                            sb.Append(Number + " ");

                        if (IsHighLightedExpirationDate)
                            sb.AppendFormat("<strong>{0}</strong>", ExpirationDate.ToShortDateString());
                        else
                            sb.Append(ExpirationDate.ToShortDateString());
                        return sb.ToString();
                    }
                }
                else if (IsWebAccount)
                {
                    if (IsHighLightedName || IsHighLightedUrl)
                    {
                        var sb = new StringBuilder();
                        if (IsHighLightedName)
                            sb.AppendFormat("<strong>{0}</strong> ", Name);
                        else
                            sb.Append(Name + " ");

                        if (IsHighLightedUrl)
                            sb.AppendFormat("<strong>{0}</strong>", Url);
                        else
                            sb.Append(Url);
                        return sb.ToString();
                    }
                }

                return Description;
            }
        }

        private bool _IsVisible = true;
        public bool IsVisible
        {
            get { return _IsVisible; }
            set
            {
                _IsVisible = value;
                RaisePropertyChanged("IsVisible");
            }
        }

        #endregion

        #region Methods

        #endregion
    }
}

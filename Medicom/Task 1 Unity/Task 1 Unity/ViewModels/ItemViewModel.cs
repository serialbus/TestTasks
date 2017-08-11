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

        public string Name
        {
            get { return Item.Name; }
        }

        #endregion
    }
}

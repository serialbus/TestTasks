using Infrastructure.Common.Models;
using Infrastructure.Common.Services;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Module.Cashbox.ViewModels
{
    [Export("ProductViewModel", typeof(ProductViewModel))]
    public class ProductViewModel : BindableBase
    {
        public ProductViewModel()
        {
            _Product = new Product();
        }

        public ProductViewModel(Product product)
        {
            _Product = product;
        }

        #region Fields And Properties

        private Product _Product = new Product();

        public Product Product
        {
            get { return _Product; }
            set
            {
                _Product = value;
                RaisePropertyChanged("Id");
                RaisePropertyChanged("Name");
                RaisePropertyChanged("Price");
                RaisePropertyChanged("Qauntity");
            }
        }

        public int Id { get { return _Product.Id; } }
        public string Name
        {
            get { return _Product.Name; }
            set
            {
                _Product.Name = value;
                OnProductChanged();
                RaisePropertyChanged("Name");
            }
        }
        public decimal Price
        {
            get { return _Product.Price; }
            set
            {
                _Product.Price = value;
                OnProductChanged();
                RaisePropertyChanged("Price");
            }
        }
        public int Qauntity
        {
            get { return _Product.Qauntity; }
            set
            {
                _Product.Qauntity = value;
                OnProductChanged();
                RaisePropertyChanged("Qauntity");
            }
        }

        #endregion

        #region Methods

        private void OnProductChanged()
        {
            DataBaseService.Repository.SaveChanges();
        }

        #endregion


    }
}

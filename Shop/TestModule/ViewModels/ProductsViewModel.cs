using Infrastructure.Common.Models;
using Infrastructure.Common.Services;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace Module.Cashbox.ViewModels
{
    [Export("ProductsViewModel", typeof(ProductsViewModel))]
    public class ProductsViewModel : BindableBase
    {
        #region Constructors

        public ProductsViewModel()
        {
            AddProductCommand = new DelegateCommand(OnAddProduct);
            DeleteProductCommand = new DelegateCommand(OnDeleteProduct, CanDeleteProduct);
            Products = new ObservableCollection<ProductViewModel>(DataBaseService.Repository.Products.Select(x => new ProductViewModel(x)));
            SelectedProduct = Products.FirstOrDefault();
        }

        #endregion

        #region Fields And Properties

        public ObservableCollection<ProductViewModel> Products { get; private set; }

        private ProductViewModel _SelectedProduct;
        public ProductViewModel SelectedProduct
        {
            get { return _SelectedProduct; }
            set
            {
                _SelectedProduct = value;
                RaisePropertyChanged("SelectedProduct");
            }
        }

        public ProductViewModel SelectedProductViewModel { get; private set; }

        #endregion

        #region Commands

        public DelegateCommand AddProductCommand { get; private set; }
        private void OnAddProduct()
        {
            var product = new ProductViewModel(DataBaseService.Repository.CreateProduct());
            Products.Add(product);
            SelectedProduct = product;
        }

        public DelegateCommand DeleteProductCommand { get; private set; }
        private void OnDeleteProduct()
        {
            DataBaseService.Repository.DeleteProduct(SelectedProduct.Product);
            Products.Remove(SelectedProduct);
            SelectedProduct = Products.FirstOrDefault();
        }
        private bool CanDeleteProduct()
        {
            return SelectedProduct != null;
        }

        #endregion
    }
}

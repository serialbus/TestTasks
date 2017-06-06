using Infrastructure.Common.Models;
using Infrastructure.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Cashbox.ViewModels
{
    [Export("OrderViewModel", typeof(OrderViewModel))]
    public class OrderViewModel: BindableBase
    {
        #region Internal types
        
        public class OrderPositionViewModel: BindableBase
        {
            #region Constructors

            public OrderPositionViewModel(OrderPosition position)
            {
                OrderPosition = position;
            }

            #endregion

            #region Fields And Properties

            public OrderPosition OrderPosition { get; private set; }

            public int? ProductId
            {
                get { return OrderPosition.ProductId; }
                set
                {
                    OrderPosition.ProductId = value;
                    RaisePropertyChanged("ProductId");
                    RaisePropertyChanged("ProductName");
                }
            }

            public string ProductName
            {
                get { return OrderPosition.Product == null ? String.Empty : OrderPosition.Product.Name; }
            }

            public int Quantity
            {
                get { return OrderPosition.Quantity; }
                set
                {
                    OrderPosition.Quantity = value;
                    RaisePropertyChanged("Quantity");
                    RaisePropertyChanged("TotalSum");
                }
            }

            public int? OrderId
            {
                get { return OrderPosition.OrderId; }
            }

            public decimal? Price
            {
                get { return OrderPosition.Product == null ? new Nullable<decimal>() : OrderPosition.Product.Price; }
            }

            public decimal? TotalSum
            {
                get
                {
                    if (OrderPosition.Product == null)
                        return null;

                    return OrderPosition.Product.Price * Quantity;
                }
            }

            #endregion
        }

        #endregion

        #region Constructor

        public OrderViewModel()
        {
            AddOrderCommand = new DelegateCommand(OnAddOrder, CanAddOrder)
                .ObservesProperty(() => IsEnabledAddOrderPosion);
            CloseOrderCommand = new DelegateCommand(OnCloseOrder, CanCloseOrder)
                .ObservesProperty(() => SelectedOrder)
                .ObservesProperty(() => IsEnabledAddOrderPosion);
            AddOrderPosionCommand = new DelegateCommand<string>(OnAddOrderPosion, CanAddOrderPosion);

            OrderPositions = new ObservableCollection<OrderPositionViewModel>();
        }

        #endregion

        #region Fields And Properties

        private Order _SelectedOrder;
        public Order SelectedOrder
        {
            get { return _SelectedOrder; }
            set
            {
                _SelectedOrder = value;
                RaisePropertyChanged("SelectedOrder");
            }
        }

        private OrderPositionViewModel _SelectedOrderPosition;
        public OrderPositionViewModel SelectedOrderPosition
        {
            get { return _SelectedOrderPosition; }
            set
            {
                _SelectedOrderPosition = value;
                RaisePropertyChanged("SelectedOrderPosition");
            }
        }

        public ObservableCollection<OrderPositionViewModel> OrderPositions { get; private set; }

        public bool IsEnabledAddOrderPosion
        {
            get { return SelectedOrder != null && SelectedOrder.IsClosed == false; }
        }

        private decimal _TotalSum;
        public decimal TotalSum
        {
            get { return _TotalSum; }
            private set
            {
                if (_TotalSum != value)
                {
                    _TotalSum = value;
                    RaisePropertyChanged("TotalSum");
                }
            }
        }

        public int? PositionsQuantity => SelectedOrder == null ? new Nullable<int>() : OrderPositions.Count;

        public string OrderStatus
        {
            get
            {
                if (SelectedOrder == null)
                    return String.Empty;

                return SelectedOrder.IsClosed ? "(Закрыт)" : "(Открыт)";
            }
        }

        #endregion

        #region Methods

        private void CalcTotalSum()
        {
            if (SelectedOrder == null)
                return;

            decimal total = 0;

            foreach(var position in OrderPositions)
            {
                total += position.Quantity * position.OrderPosition.Product.Price;
            }

            TotalSum = total;
        }

        #endregion

        #region Commands

        public DelegateCommand AddOrderCommand { get; private set; }
        private void OnAddOrder()
        {
            SelectedOrder = DataBaseService.Repository.CreateOrder();
            OrderPositions.Clear();
            CalcTotalSum();
            RaisePropertyChanged("PositionsQuantity");
            RaisePropertyChanged("OrderStatus");
            RaisePropertyChanged("IsEnabledAddOrderPosion");
        }
        private bool CanAddOrder()
        {
            return SelectedOrder == null || SelectedOrder.IsClosed == true;
        }

        public DelegateCommand CloseOrderCommand { get; private set; }
        private void OnCloseOrder()
        {
            foreach (var position in OrderPositions)
            {
                SelectedOrder.OrderPositions.Add(position.OrderPosition);

            }
            SelectedOrder.EndTransaction = DateTime.Now;
            SelectedOrder.IsClosed = true;
            DataBaseService.Repository.SaveChanges();
            RaisePropertyChanged("OrderStatus");
            RaisePropertyChanged("IsEnabledAddOrderPosion");
        }
        private bool CanCloseOrder()
        {
            return SelectedOrder != null && SelectedOrder.IsClosed == false;
        }

        public DelegateCommand<string> AddOrderPosionCommand { get; private set; }
        private void OnAddOrderPosion(string text)
        {
            int productCode;

            if (int.TryParse(text, out productCode))
            {
                // Ищем товар с данным кодом в базе
                var product = DataBaseService.Repository.Products.FirstOrDefault(x => x.Id == productCode);

                if (product != null)
                {
                    // Ищем позицию с данным товарам, которая могла быть добавлена ранее, если находим инкиментируем количество иначе создаём новую позицию
                    var position = OrderPositions.FirstOrDefault(x => x.ProductId == product.Id);

                    if (position != null)
                    {
                        ++position.Quantity;
                        SelectedOrderPosition = position;
                    }
                    else
                    {
                        var newPosition = new OrderPosition
                        {
                            Order = SelectedOrder,
                            OrderId = SelectedOrder.Id,
                            Product = product,
                            ProductId = product.Id,
                            Quantity = 1
                        };
                        SelectedOrderPosition = new OrderPositionViewModel(newPosition);
                        OrderPositions.Add(SelectedOrderPosition);
                    }
                    CalcTotalSum();
                    RaisePropertyChanged("PositionsQuantity");
                }
                else
                {
                    //TODO: Ошибка Товар не найден
                }
            }
            else
            {
                //TODO: Ошибка Неверный ввод
            }
        }
        private bool CanAddOrderPosion(string text)
        {
            return IsEnabledAddOrderPosion;
            //return true;
        }

        #endregion
    }
}

using System;

namespace Infrastructure.Common.Services
{
    /// <summary>
    /// Единый сервис для всех офисов для генерации уникальных последовательных
    /// номеров заказов замеров
    /// </summary>
    public class NumbersGeneratorService : INumbersGeneratorService
    {

        /// <summary>
        /// Максимальный номер заказа, после которого счётчик обнуляется
        /// </summary>
        public const int MAXNUMBER = 999999;

        #region Constructors

        public NumbersGeneratorService()
        {
        }

        #endregion

        #region Fields And Properties

        private static int LastNumber = 0;

        private static ServiceStatus _Status = ServiceStatus.Ready;

        public ServiceStatus Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnServiceChangedStatus();
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Генерирует новый уникальный последовательный номер заказа
        /// </summary>
        /// <returns></returns>
        public int GenerateOrderNumber()
        {
            var currentNumber = LastNumber + 1;

            if (currentNumber > MAXNUMBER)
            {
                Status = ServiceStatus.OutOfService;
                throw new InvalidOperationException("Счётчик номеров сервиса переполнен");
            }

            return LastNumber = currentNumber;
        }

        private void OnServiceChangedStatus()
        {
            ServiceChagedStatus?.Invoke(this, new EventArgs());
        }

        #endregion

        #region Events

        public event EventHandler ServiceChagedStatus;

        #endregion
    }
}

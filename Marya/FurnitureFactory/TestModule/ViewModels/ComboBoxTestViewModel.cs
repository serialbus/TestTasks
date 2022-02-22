using Microsoft.Practices.Unity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestModule.Models;

namespace TestModule.ViewModels
{
    public class ComboBoxTestViewModel : BindableBase
    {
        #region Constructors

        public ComboBoxTestViewModel(IUnityContainer container)
        {
            var list = new List<MyClass>
            {
                new MyClass { Name = "Первый" },
                new MyClass { Name = "Второй" },
                new MyClass { Name = "Третий" }
            };

            MyClassList = new ObservableCollection<MyClass>(list);
            _SelectedMyClass = MyClassList[1];
            OnPropertyChanged(() => SelectedMyClass);
            _SelectedUid = MyClassList[1].Uid;
            OnPropertyChanged(() => SelectedUid);
        }

        #endregion

        #region Fields And Properties

        public ObservableCollection<MyClass> MyClassList { get; private set; }

        private MyClass _SelectedMyClass;
        public MyClass SelectedMyClass
        {
            get { return _SelectedMyClass; }
            set
            {
                if (false)
                {
                    _SelectedMyClass = value;
                }
                OnPropertyChanged(() => SelectedMyClass);
                //RaisePropertyChanged(() => SelectedMyClass);
            }
        }

        private Guid _SelectedUid;
        public Guid SelectedUid
        {
            get { return _SelectedUid; }
            set
            {
                if (false)
                {
                    _SelectedUid = value;
                }
                OnPropertyChanged(() => SelectedUid);
            }
        }

        #endregion

        #region Event Handlers
        #endregion
    }
}

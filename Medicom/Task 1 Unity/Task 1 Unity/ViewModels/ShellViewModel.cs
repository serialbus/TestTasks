using Infrastructure.Common.Events.DAL;
using Infrastructure.Common.Models.Configuration;
using Infrastructure.Common.Models.DAL;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows;
using System.Windows.Input;

namespace Medicom.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        #region Consturctors

        public ShellViewModel(IUnityContainer container)
        {
        }

        #endregion

        #region Fields And Properties

        private string _title = "Задание 1";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion

        #region Methods

        #endregion

        #region Commands

        #endregion
    }
}

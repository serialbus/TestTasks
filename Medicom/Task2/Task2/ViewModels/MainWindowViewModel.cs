﻿using Prism.Mvvm;

namespace Medicom.Task2.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		private string _title = "Prism Unity Application";
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public MainWindowViewModel()
		{

		}
	}
}

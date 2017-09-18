﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestModule.ViewModels;

namespace TestModule.Views
{
	/// <summary>
	/// Interaction logic for ComboBoxTestView.xaml
	/// </summary>
	public partial class ComboBoxTestView : UserControl
	{
		public ComboBoxTestView(ComboBoxTestViewModel vm)
		{
			InitializeComponent();
			DataContext = vm;
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			e.AddedItems.Clear();
			e.Handled = true;
			return;
		}
	}
}

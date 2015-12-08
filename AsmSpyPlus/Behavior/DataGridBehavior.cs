using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AsmSpyPlus.Behavior
{
	public static class DataGridBehavior
	{
		public static readonly DependencyProperty UseBrowsableAttributeOnColumnProperty =
			DependencyProperty.RegisterAttached("UseBrowsableAttributeOnColumn", 
			typeof(bool),
			typeof(DataGridBehavior), 
			new UIPropertyMetadata(false, UseBrowsableAttributeOnColumnChanged));

		public static bool GetUseBrowsableAttributeOnColumn(DependencyObject obj)
		{
			return (bool)obj.GetValue(UseBrowsableAttributeOnColumnProperty);
		}

		public static void SetUseBrowsableAttributeOnColumn(DependencyObject obj, bool val)
		{
			obj.SetValue(UseBrowsableAttributeOnColumnProperty, val);
		}

		private static void UseBrowsableAttributeOnColumnChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			var dataGrid = obj as DataGrid;
			if (dataGrid != null)
			{
				if ((bool) e.NewValue)
				{
					dataGrid.AutoGeneratingColumn += DataGridOnAutoGeneratingColumn;
				}
				else
				{
					dataGrid.AutoGeneratingColumn -= DataGridOnAutoGeneratingColumn;
				}
			}
		}

		private static void DataGridOnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			var propDesc = e.PropertyDescriptor as PropertyDescriptor;
			
			if (propDesc != null)
			{
				foreach(Attribute att in propDesc.Attributes)
				{
					var browsableAttribute = att as BrowsableAttribute;
					if (browsableAttribute != null)
					{
						if (! browsableAttribute.Browsable)
						{
							e.Cancel = true;
						}
					}
				}
			}
		}
	}
}

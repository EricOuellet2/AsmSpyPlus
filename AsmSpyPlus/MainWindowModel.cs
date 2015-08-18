using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AsmSpyPlus.AssemblyInfo;

namespace AsmSpyPlus
{
	public class MainWindowModel : INotifyPropertyChanged
	{
		// ******************************************************************
		public event PropertyChangedEventHandler PropertyChanged;

		// ******************************************************************
		private AssemblyAnalysis _assemblyAnalysis;
		private bool _showSystemAssemblies = true;

		// ******************************************************************
		public AssemblyAnalysis AssemblyAnalysis
		{
			get { return _assemblyAnalysis; }
			set
			{
				if (_assemblyAnalysis == value) return;

				_assemblyAnalysis = value;
				NotifyPropertyChanged();
			}
		}

		// ******************************************************************
		public bool ShowSystemAssemblies
		{
			get { return _showSystemAssemblies; }
			set
			{
				if (_showSystemAssemblies == value) return;

				_showSystemAssemblies = value;
				NotifyPropertyChanged();
			}
		}

		// ******************************************************************
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// ******************************************************************
		private List<String> _duplicateAssemblies = null;

		public List<string> DuplicateAssemblies
		{
			get { return _duplicateAssemblies; }
			set
			{
				if (_duplicateAssemblies == value) return;

				_duplicateAssemblies = value;
				NotifyPropertyChanged();
			}
		}

		// ******************************************************************

	}
}

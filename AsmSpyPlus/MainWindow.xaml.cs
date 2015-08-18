using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AsmSpyPlus.AssemblyInfo;
using Cursors = System.Windows.Input.Cursors;
using Path = System.IO.Path;

namespace AsmSpyPlus
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainWindowModel _mainWindowModel = null;

		public MainWindow()
		{
			InitializeComponent();
			Model.PropertyChanged += ModelOnPropertyChanged;
		}

		private void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "ShowSystemAssemblies")
			{
				if (Model.ShowSystemAssemblies)
				{
					var cvs = DataGridAssemblies.ItemsSource as CollectionViewSource;
					if (cvs != null)
					{
						// To do....
					}
				}
			}
		}

		private void CmdBrowse_Click(object sender, RoutedEventArgs e)
		{
			var folderBrowser = new System.Windows.Forms.FolderBrowserDialog();

			string appDataConfigFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Assembly.GetEntryAssembly().GetName().Name + ".txt");

			if (File.Exists(appDataConfigFilename))
			{
				folderBrowser.SelectedPath = File.ReadAllText(appDataConfigFilename);
			}

			folderBrowser.ShowDialog();
			if (Directory.Exists(folderBrowser.SelectedPath))
			{
				Mouse.OverrideCursor = Cursors.Wait;

				AssemblyAnalysis aa = AssemblyAnalysis.AnalyseFolder(folderBrowser.SelectedPath);
				Model.AssemblyAnalysis = aa;

				HashSet<string> duplicateAssemblies = new HashSet<string>();
				foreach (ReferencedAssembly refAsmIter in aa.ReferencedAssemblies)
				{

					foreach (ReferencedAssembly refAsmInner in aa.ReferencedAssemblies)
					{
						if (refAsmIter.AssemblyName.Name == refAsmInner.AssemblyName.Name)
						{
							if (!refAsmIter.AssemblyName.Version.Equals(refAsmInner.AssemblyName.Version))
							{
								duplicateAssemblies.Add(refAsmIter.AssemblyName.Name);
							}
						}
					}
				}

				Model.DuplicateAssemblies = duplicateAssemblies.ToList();

				File.WriteAllText(appDataConfigFilename, folderBrowser.SelectedPath);

				Mouse.OverrideCursor = Cursors.Arrow;
			}
		}

		MainWindowModel Model
		{
			get
			{
				if (_mainWindowModel == null)
				{
					_mainWindowModel = DataContext as MainWindowModel;
				}

				return _mainWindowModel;
			}
		}
	}
}

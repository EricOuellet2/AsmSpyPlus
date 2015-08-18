using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AsmSpyPlus.Native;

namespace AsmSpyPlus.AssemblyInfo
{
	public class AssemblyAnalysis
	{
		// ******************************************************************
		public string DirectoryPath { get; set; }
		public string Result { get; private set; }
		public ObservableCollection<ReferencedAssembly> ReferencedAssemblies { get; set; }

		// ******************************************************************
		public static AssemblyAnalysis AnalyseFolder(string directoryPath)
		{
			var assemblyAnalysis = new AssemblyAnalysis(directoryPath);

			DirectoryInfo directoryInfo = null;
			try
			{
				directoryInfo = new DirectoryInfo(directoryPath);
			}
			catch (Exception)
			{
			}

			if (directoryInfo == null)
			{
				assemblyAnalysis.Result = "Unable to retreive directory information.";
			}
			else if (!directoryInfo.Exists)
			{
				assemblyAnalysis.Result = "Directory does not exists.";
			}
			else
			{
				var assemblyFiles = directoryInfo.GetFiles("*.dll").Concat(directoryInfo.GetFiles("*.exe"));
				if (!assemblyFiles.Any())
				{
					assemblyAnalysis.Result = "No .dll or .exe files found.";
				}
				else
				{
					var sbResult = new StringBuilder();

					//	var assemblies = new Dictionary<string, IList<ReferencedAssembly>>();
					foreach (var fileInfo in assemblyFiles.OrderBy(asm => asm.Name))
					{
						Assembly assembly = null;
						try
						{
							if (!fileInfo.IsAssembly())
							{
								continue;
							}
							assembly = Assembly.ReflectionOnlyLoadFrom(fileInfo.FullName);
						}
						catch (Exception ex)
						{
							sbResult.Append(string.Format("Failed to load assembly '{0}': {1}", fileInfo.FullName, ex.Message));
							sbResult.Append(Environment.NewLine);
							continue;
						}

						foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
						{
							assemblyAnalysis.AddReferencedAssemblyFor(assembly, referencedAssembly);
						}
					}

					//if (onlyConflicts)
					//	Console.WriteLine("Detailing only conflicting assembly references.");

					//foreach (var assembly in assemblies)
					//{
					//	if (skipSystem && (assembly.Key.StartsWith("System") || assembly.Key.StartsWith("mscorlib"))) continue;

					//	if (!onlyConflicts
					//		|| (onlyConflicts && assembly.Value.GroupBy(x => x.VersionReferenced).Count() != 1))
					//	{
					//		Console.ForegroundColor = ConsoleColor.White;
					//		Console.Write("Reference: ");
					//		Console.ForegroundColor = ConsoleColor.Gray;
					//		Console.WriteLine("{0}", assembly.Key);

					//		var referencedAssemblies = new List<Tuple<string, string>>();
					//		var versionsList = new List<string>();
					//		var asmList = new List<string>();
					//		foreach (var referencedAssembly in assembly.Value)
					//		{
					//			var s1 = referencedAssembly.VersionReferenced.ToString();
					//			var s2 = referencedAssembly.ReferencedBy.GetName().Name;
					//			var tuple = new Tuple<string, string>(s1, s2);
					//			referencedAssemblies.Add(tuple);
					//		}

					//		foreach (var referencedAssembly in referencedAssemblies)
					//		{
					//			if (!versionsList.Contains(referencedAssembly.Item1))
					//			{
					//				versionsList.Add(referencedAssembly.Item1);
					//			}
					//			if (!asmList.Contains(referencedAssembly.Item1))
					//			{
					//				asmList.Add(referencedAssembly.Item1);
					//			}
					//		}

					//		foreach (var referencedAssembly in referencedAssemblies)
					//		{
					//			var versionColor = ConsoleColors[versionsList.IndexOf(referencedAssembly.Item1)%ConsoleColors.Length];

					//			Console.ForegroundColor = versionColor;
					//			Console.Write("   {0}", referencedAssembly.Item1);

					//			Console.ForegroundColor = ConsoleColor.White;
					//			Console.Write(" by ");

					//			Console.ForegroundColor = ConsoleColor.Gray;
					//			Console.WriteLine("{0}", referencedAssembly.Item2);
					//		}

					//		Console.WriteLine();
					//	}

					//}

					sbResult.Append("Loading folder assemblies completed!");
					assemblyAnalysis.Result = sbResult.ToString();
				}
			}

			return assemblyAnalysis;
		}

		// ******************************************************************
		public AssemblyAnalysis(string directoryPath)
		{
			DirectoryPath = directoryPath;
			ReferencedAssemblies = new ObservableCollection<ReferencedAssembly>();
		}

		// ******************************************************************
		private void AddReferencedAssemblyFor(Assembly assemblyReferer, AssemblyName assemblyName)
		{
			var existingReferencedAsm = ReferencedAssemblies.FirstOrDefault(refAsm => refAsm.UniqueName == ReferencedAssembly.GetUniqueNameFromAssemblyName(assemblyName));
			if (existingReferencedAsm == null)
			{
				existingReferencedAsm = new ReferencedAssembly(assemblyName);
				this.ReferencedAssemblies.Add(existingReferencedAsm);
			}

			existingReferencedAsm.Referers.Add(assemblyReferer);
		}

		// ******************************************************************
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsmSpyPlus
{
	public class AssemblyDetails
	{
		public AssemblyDetails(Assembly assembly)
		{
			Assembly = assembly;

			var sbModulesInfo = new StringBuilder();

			bool firstLine = true;
			foreach (Module module in assembly.Modules)
			{
				if (firstLine)
				{
					firstLine = false;
				}
				else
				{
					sbModulesInfo.AppendLine();
				}

				PortableExecutableKinds pek;
				ImageFileMachine ifm;
				module.GetPEKind(out pek, out ifm);

				PortableExecutableKinds = pek;
				ImageFileMachine = ifm;

				sbModulesInfo.Append(String.Format("Module: {0}, executableKind: {1}, imageFileMachine: {2}; ", module.Name, pek, ifm));
			}

			ModulesInfo = sbModulesInfo.ToString();
		}

		[Browsable(false)]
		public Assembly Assembly { get; set; }

		
		public string FullName
		{
			get { return Assembly.FullName; }
			
		}

		public string Location
		{
			get { return Assembly.Location; }

		}

		public PortableExecutableKinds PortableExecutableKinds { get; set; }
		public ImageFileMachine ImageFileMachine { get; set; }
		public String ModulesInfo { get; private set; }
	}
}

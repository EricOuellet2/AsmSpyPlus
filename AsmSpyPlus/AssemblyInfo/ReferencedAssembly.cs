using System;
using System.Collections.Generic;
using System.Reflection;

namespace AsmSpyPlus.AssemblyInfo
{
	public class ReferencedAssembly
	{
        public AssemblyName AssemblyName { get; private set; }
		
		public List<Assembly> Referers { get; private set; }

        public ReferencedAssembly(AssemblyName assemblyName)
        {
			AssemblyName = assemblyName;
			Referers = new List<Assembly>();
        }

		public string UniqueName
		{
			get { return GetUniqueNameFromAssemblyName(AssemblyName); }
		}

		public static string GetUniqueNameFromAssemblyName(AssemblyName assemblyName)
		{
			return assemblyName.FullName;
		}



	}
}

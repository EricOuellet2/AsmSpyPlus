using System;
using System.Collections.Generic;
using System.Reflection;
using AsmSpy.Native;

namespace AsmSpyPlus.AssemblyInfo
{
	public class ReferencedAssembly
	{
        public AssemblyName AssemblyName { get; private set; }
		
		// public MachineType MachineType { get; set; }

		public List<AssemblyDetails> Referers { get; private set; }

		public ReferencedAssembly(AssemblyName assemblyName)
        {
			AssemblyName = assemblyName;
			Referers = new List<AssemblyDetails>();
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

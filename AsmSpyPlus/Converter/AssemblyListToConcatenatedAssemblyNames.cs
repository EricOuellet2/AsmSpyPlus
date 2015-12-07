using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AsmSpyPlus.Converter
{
	public class AssemblyListToConcatenatedAssemblyNames : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var assemblyList = value as List<AssemblyDetails>;
			if (assemblyList != null)
			{
				var sb = new StringBuilder();
				bool first = true;
				foreach (AssemblyDetails asm in assemblyList)
				{
					if (first)
					{
						first = false;
					}
					else
					{
						sb.Append(Environment.NewLine);
					}

					sb.Append(asm.Assembly.GetName().Name);
				}

				return sb.ToString();
			}

			return "?";
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

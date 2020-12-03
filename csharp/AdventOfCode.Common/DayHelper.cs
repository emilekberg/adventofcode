using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace AdventOfCode.Common
{
	public static class DayHelper
	{
		public static void RegisterAssembly(Assembly assembly)
		{
			Console.WriteLine($"registered {assembly.FullName}");
		}
		public static List<Type> GetDaysFromAssemblies()
		{
			var baseType = typeof(IDay);
			var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(p => baseType.IsAssignableFrom(p))
				.Where(p => p != baseType);
			return types.ToList();
		}
		
	}
}

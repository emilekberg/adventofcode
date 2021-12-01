using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace AdventOfCode.Common;

public static class DayHelper
{
    private static readonly Type _baseType = typeof(IDay);
    public static void RegisterAssembly(Assembly assembly)
    {
        Console.WriteLine($"registered {assembly.FullName}");
    }
    public static List<Type> GetDaysFromAssemblies()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => _baseType.IsAssignableFrom(p))
            .Where(p => p != _baseType);
        return types.ToList();
    }

}

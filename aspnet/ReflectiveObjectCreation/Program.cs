using System;
using System.Reflection;
using System.Linq;
using RVTR.Lodging.ObjectModel.Models;
using System.Collections.Generic;

namespace ReflectiveObjectCreation
{
  class Program
  {
    private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
      return
        assembly.GetTypes()
                .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                .ToArray();
    }
    static void Main(string[] args)
    {
      Type[] typelist = GetTypesInNamespace(Assembly.Load("RVTR.Lodging.ObjectModel.Models"), "RVTR.Lodging.ObjectModel.Models");
      for (int i = 0; i < typelist.Length; i++)
      {
        Console.WriteLine(typelist[i].Name);
      }
    }
  }
}

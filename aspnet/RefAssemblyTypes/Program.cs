using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Infrastructure;
namespace RefAssemblyTypes
{
    class Program
    {
        static void Main(string[] args)
        {

      foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
      {
        Console.WriteLine(assemblyName);
        Assembly asm = Assembly.Load(assemblyName);

        var controlleractionlist = asm.GetTypes()
        .Where(type => typeof(Microsoft.AspNetCore.Mvc.ControllerBase).IsAssignableFrom(type));
        
        foreach (var c in controlleractionlist)
        {
          MethodBase Mymethodbase = c.GetMethod("Mymethod");

          // Display the method name.
          Console.WriteLine("Mymethodbase = " + Mymethodbase);
          Console.WriteLine(c);
        }
      }
      
      }
    }
    }

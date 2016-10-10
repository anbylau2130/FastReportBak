using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace USP.Utility
{
    public static class AssemblyUtil
    {
        public static IEnumerable<Type> GetAllTypesEnum()
        {
            List<Type> result = new List<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                result.AddRange(GetTypesEnum(assembly));
            }
            return result;
        }

        public static IEnumerable<Type> GetAllExportedTypesEnum()
        {
            List<Type> result = new List<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                result.AddRange(GetExportedTypesEnum(assembly));
            }
            return result;
        }

        public static IEnumerable<AssemblyName> GetAllReferencedAssembliesEnum()
        {
            List<AssemblyName> result = new List<AssemblyName>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                result.AddRange(GetReferencedAssembliesEnum(assembly));
            }
            return result;
        }

        public static IEnumerable<Module> GetAllModulesEnum()
        {
            List<Module> result = new List<Module>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                result.AddRange(GetModulesEnum(assembly));
            }
            return result.ToArray();
        }

        public static IEnumerable<AssemblyName> GetReferencedAssembliesEnum(this Assembly assembly)
        {
            return assembly.GetReferencedAssemblies();
        }

        public static IEnumerable<Module> GetModulesEnum(this Assembly assembly)
        {
            return assembly.GetModules();
        }


        public static IEnumerable<Type> GetTypesEnum(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        public static IEnumerable<Type> GetExportedTypesEnum(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetExportedTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        public static IEnumerable<Type> GetAllSubTypesEnum(Type type)
        {
            return (from t in GetAllTypesEnum()
                    where type.IsAssignableFrom(t) && !t.IsAbstract
                    orderby t.FullName
                    select t);
        }

        public static IEnumerable<MemberInfo> GetMemberInfoEnum(Type type)
        {
            return type.GetMembers();
        }

        public static IEnumerable<MethodInfo> GetMethodInfoEnum(Type type)
        {
            return type.GetMethods();
        }

        public static IEnumerable<PropertyInfo> GetPropertyInfoEnum(Type type)
        {
            return type.GetProperties();
        }

        public static IEnumerable<Attribute> GetCustomAttributes(Type type)
        {
            return type.GetCustomAttributes();
        }
    }
}
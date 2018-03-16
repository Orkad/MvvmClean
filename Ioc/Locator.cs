using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MvvmClean.Ioc
{
    /// <summary>
    /// Classe statique permetant l'initialisation et l'accès du type de locator pour l'application entière
    /// </summary>
    public static class Locator
    {
        public static ILocator Current { get; private set; }

        public static bool IsLoaded => Current != null;

        public static void SetLocator<T>() where T:ILocator, new()
        {
            Current = Activator.CreateInstance<T>();
        }
    }
}

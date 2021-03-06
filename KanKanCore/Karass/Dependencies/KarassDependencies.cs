using System;
using System.Collections.Generic;
using KanKanCore.Karass.Interface;

namespace KanKanCore.Karass.Dependencies
{
    // NOTE: Highly recommended you use an established DI framework and map functionality to IDependencies interface;
    public class KarassDependencies : IDependencies
    {
        private readonly Dictionary<Type, Func<dynamic>> _dependency = new Dictionary<Type, Func<dynamic>>();

        public T Get<T>() where T : class
        {
            return _dependency[typeof(T)]() as T;
        }

        public void Register<T>(Func<dynamic> resolver)
        {
            _dependency.Add(typeof(T), resolver);
        }
    }
}
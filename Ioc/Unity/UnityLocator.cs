using System;
using Unity;

namespace MvvmClean.Ioc.Unity
{
    public class UnityLocator : ILocator, IDisposable
    {
        /// <summary>
        /// Conteneur Unity.
        /// </summary>
        private readonly IUnityContainer _container;

        /// <summary>
        /// Constructeur du conteneur unity
        /// </summary>
        public UnityLocator()
        {
            _container = new UnityContainer();
            _container.RegisterType<ILocator, UnityLocator>();
        }

		/// <summary>
		/// Récupère la dépendance demandée.
		/// </summary>
		/// <typeparam name="T">Type de la dépendance.</typeparam>
		/// <returns>La dépendance demandée.</returns>
		public T Resolve<T>()
		{
			return _container.Resolve<T>();
		}

		/// <summary>
		/// Récupère la dépendance demandée.
		/// </summary>
		/// <param name="ty">Type de la dépendance.</param>
		/// <returns>La dépendance demandée.</returns>
		public object Resolve(Type ty)
		{
			return _container.Resolve(ty, null);
		}

		/// <summary>
		/// Enregistre une dépendance dans le conteneur
		/// Utilisé pour les services.
		/// </summary>
		/// <typeparam name="TFrom">Interface de la dépendance.</typeparam>
		/// <typeparam name="TTo">Type de la dépendance.</typeparam>
		public void Register<TFrom, TTo>() where TTo : TFrom
		{
		    _container.RegisterType<TFrom, TTo>();
		}

		/// <summary>
		/// Enregistre une dépendance dans le conteneur
		/// Utilisé pour les viewmodels.
		/// </summary>
		/// <typeparam name="T">Type a enregistrer</typeparam>
		public void Register<T>()
		{
		    _container.RegisterType<T>();
		}

        /// <summary>
        /// Enregistre une dépendance dans le conteneur
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
	    public void Register<T>(T instance) where T : class
        {
            _container.RegisterInstance<T>(instance);
        }

	    public void Dispose()
	    {
	        _container?.Dispose();
	    }
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MvvmClean.Ioc
{
    /// <summary>
    /// Fournit l'injection de dépendance
    /// </summary>
    public interface ILocator
    {
        /// <summary>
        /// Récupère la dépendance demandée.
        /// </summary>
        /// <typeparam name="T">Type de la dépendance.</typeparam>
        /// <returns>La dépendance demandée.</returns>
        T Resolve<T>();

        /// <summary>
        /// Récupère la dépendance demandée.
        /// </summary>
        /// <param name="ty">Type de la dépendance.</param>
        /// <returns>La dépendance demandée.</returns>
        object Resolve(Type ty);

        /// <summary>
        /// Enregistre une dépendance dans le conteneur
        /// Utilisé pour les services.
        /// </summary>
        /// <typeparam name="TFrom">Interface de la dépendance.</typeparam>
        /// <typeparam name="TTo">Type de la dépendance.</typeparam>
        void Register<TFrom, TTo>() where TTo : TFrom;

        /// <summary>
        /// Enregistre une dépendance dans le conteneur
        /// Utilisé pour les viewmodels.
        /// </summary>
        /// <typeparam name="T">Type a enregistrer</typeparam>
        void Register<T>();

        /// <summary>
        /// Enregistre une dépendance dans le conteneur
        /// </summary>
        /// <typeparam name="T">Type de l'instance à enregistrer</typeparam>
        /// <param name="instance">instance à enregistrer</param>
        void Register<T>(T instance) where T : class;
    }
}

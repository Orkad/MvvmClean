using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SampleWpf.Services
{
    /// <summary>
    /// Create Read Update Delete d'une donnée de type T
    /// </summary>
    /// <typeparam name="T">type de la donnée</typeparam>
    public interface ICrudService<T>
    {
        /// <summary>
        /// Insert une nouvelle donnée et en retourne la clef
        /// </summary>
        /// <param name="item"></param>
        int Create(T item);

        /// <summary>
        /// Récupère toutes les données
        /// </summary>
        /// <returns></returns>
        List<T> Read();

        /// <summary>
        /// Récupère la donnée correspondant à la clef
        /// </summary>
        /// <param name="key"></param>
        T Read(int key);

        /// <summary>
        /// Met à jour la donnée
        /// </summary>
        /// <param name="item">donnée à mettre à jour</param>
        void Update(T item);

        /// <summary>
        /// Supprime la donnée correspondant à la clef
        /// </summary>
        /// <param name="key"></param>
        void Delete(int key);
    }
}

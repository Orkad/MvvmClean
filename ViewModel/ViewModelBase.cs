using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmClean.ViewModel
{
    /// <summary>
    /// Classe de base pour ViewModel permetant la notification d'un changement d'une propriété à la vue
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifie le changement d'un propriété
        /// </summary>        
        /// <param name="propertyName">propriété a préciser si cette méthode se trouve en dehors de la propriété
        ///  sur laquelle on souhaite notifier le changement</param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Met a jour la propriété en notifiant son changement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">le stockage de la propriété en référence</param>
        /// <param name="value">la nouvelle valeur</param>
        /// <param name="propertyName">propriété a préciser si cette méthode se trouve en dehors de la propriété
        ///  sur laquelle on souhaite notifier le changement</param>
        /// <returns></returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }
    }
}

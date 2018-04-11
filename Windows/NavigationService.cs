using System;
using System.Collections.Generic;
using System.Windows.Controls;
using MvvmClean.View.Navigation;
using MvvmClean.ViewModel.Navigation;

namespace MvvmClean.Windows
{
    public class NavigationService:INavigationService
    {
        /// <summary>
        /// Association d'une clef string avec un Type de Page
        /// </summary>
        private readonly Dictionary<object, Type> _pages = new Dictionary<object, Type>();
        
        /// <summary>
        /// Historique de navigation
        /// </summary>
        private readonly Stack<Page> _navStack = new Stack<Page>();

        /// <summary>
        /// Conteneur sur laquelle sera basé le service de navigation
        /// </summary>
        private readonly ContentControl _contentControl;

        /// <summary>
        /// Création du service de navigation sur le 'ContentControl' qui sera le conteneur de navigation
        /// </summary>
        /// <param name="contentControl"></param>
        public NavigationService(ContentControl contentControl)
        {
            _contentControl = contentControl;
        }

        /// <summary>
        /// Reviens à la page précédente si possible, en passant le paramètre au datacontext 
        /// via l'interface <see cref="INavigable"/>. Ne fait rien si aucune page précédente.
        /// </summary>
        /// <param name="parameter">paramètre à faire passer au DataContext
        /// via l'interface <see cref="INavigable"/></param>
        public void GoBack(object parameter = null)
        {
            if (_navStack.Count > 0)
            {
                var oldContentControl = _navStack.Pop();
                _contentControl.Content = oldContentControl;

                // envoi du paramètre si le DataContext de la page précédente implémente INavigable
                var navigable = oldContentControl.Content as INavigable;
                navigable?.OnBackedHere(parameter);
            }
        }

        /// <summary>
        /// Navigation vers la page associée à la clef avec passage de paramètre si le ViewModel correspondant
        /// implémente l'interface INavigable
        /// </summary>
        /// <param name="key">clef paramètrée pour la page</param>
        /// <param name="parameter">paramètre à transmettre au ViewModel s'il implémente INavigable</param>
        public void NavigateTo(object key, object parameter = null)
        {
            if (!_pages.ContainsKey(key))
            {
                throw new ArgumentException($"Page introuvable: {key} ", nameof(key));
            }
            lock (_pages)
            {
                // historisation de l'ancienne page
                var oldPage = _contentControl.Content as Page;
                if (oldPage != null)
                {
                    _navStack.Push(oldPage);
                }

                // Affection de la vue
                var instance = (Page)Activator.CreateInstance(_pages[key]);
                _contentControl.Content = instance; //n'affecte pas directement le Content (chargement interne a l'UI async)

                var navigable = instance?.DataContext as INavigable;
                navigable?.OnNavigatedHere(parameter);
            }
        }

        /// <summary>
        /// Configuration de la navigation
        /// </summary>
        /// <typeparam name="TPage">La page a associer</typeparam>
        /// <param name="key">La clef associée</param>
        public void Configure<TPage>(string key) where TPage : Page
        {
            _pages.Add(key, typeof(TPage));
        }
    }
}

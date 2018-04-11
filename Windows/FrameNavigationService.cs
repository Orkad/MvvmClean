using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using MvvmClean.Ioc;
using MvvmClean.View.Navigation;
using MvvmClean.ViewModel.Navigation;

namespace MvvmClean.Windows
{
    /// <summary>
    /// Service de navigation par Frame de PresentationFramework de microsoft (Auteur: Nicolas Gidon)
    /// </summary>
    public class FrameNavigationService : INavigationService
    {
        /// <summary>
        /// Association d'une clef string avec un Type de Page
        /// </summary>
        private readonly Dictionary<object, Type> _pages = new Dictionary<object, Type>();

        /// <summary>
        /// Association d'une clef string avec un type de contexte (ViewModel)
        /// </summary>
        private readonly Dictionary<object, Type> _contexts = new Dictionary<object, Type>();

        /// <summary>
        /// Historique de navigation
        /// </summary>
        private readonly Stack<Page> _navStack = new Stack<Page>();

        /// <summary>
        /// Frame sur laquelle sera basé le service de navigation
        /// </summary>
        private readonly ContentControl _contentControl;

        /// <summary>
        /// Permet de dispatcher sur le thread UI
        /// </summary>
        private readonly Dispatcher _uiDispatcher;

        /// <summary>
        /// Créé une instance de service de navigation pour la frame passé en paramètre
        /// A appeller depuis le thread UI uniquement
        /// </summary>
        /// <param name="contentControl">Conteneur cible de la navigation</param>
        public FrameNavigationService(ContentControl contentControl)
        {
            if(!Locator.IsLoaded)
            {
                throw new ArgumentException("Le ServiceLocator doit être initialisé");
            }
            _contentControl = contentControl;
            _uiDispatcher = Dispatcher.CurrentDispatcher;
        }

        /// <summary>
        /// Reviens à la page précédente si possible, en passant le paramètre au datacontext 
        /// via l'interface <see cref="INavigable"/>. Ne fait rien si aucune page précédente.
        /// </summary>
        /// <param name="parameter">paramètre à faire passer au DataContext
        /// via l'interface <see cref="INavigable"/></param>
        public void GoBack(object parameter)
        {
            if (_navStack.Count > 0)
            {
                Page oldPage = _navStack.Pop();
                _contentControl.Content = oldPage;

                // envoi du paramètre si le DataContext implémente INavigable
                var navigable = oldPage.Content as INavigable;
                navigable?.OnBackedHere(parameter);
            }
        }

        /// <summary>
        /// Navigation vers la page associée à la clef avec passage de paramètre si le ViewModel correspondant
        /// implémente l'interface INavigable
        /// </summary>
        /// <param name="key">clef paramètrée pour la page</param>
        /// <param name="parameter">paramètre à transmettre au ViewModel s'il implémente INavigable</param>
        public virtual void NavigateTo(object key, object parameter)
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
                var page = (Page)Activator.CreateInstance(_pages[key]);
                _contentControl.Content = page; //n'affecte pas directement le Content (chargement interne a l'UI async)

                object viewModel;
                // Chargement du ViewModel dans une task
                Task.Run(() =>
                {
                    // Passage de paramètre si le ViewModel implémente INavigable
                    viewModel = Locator.Current.Resolve(_contexts[key]);
                    var nav = viewModel as INavigable;
                    nav?.OnNavigatedHere(parameter);
                    _uiDispatcher.Invoke(() => page.DataContext = viewModel);
                });
            }
        }

        /// <summary>
        /// Configuration de la navigation
        /// </summary>
        /// <typeparam name="TContext">Le contexte de donnée a appliquer à la page</typeparam>
        /// <typeparam name="TPage">La page a associer</typeparam>
        /// <param name="key">La clef associée</param>
        public void Configure<TContext,TPage>(object key)where TPage:Page
        {
            _pages.Add(key, typeof(TPage));
            _contexts.Add(key, typeof(TContext));
        }
    }
}

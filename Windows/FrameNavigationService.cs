using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
<<<<<<< HEAD:Windows/FrameNavigationService.cs
using MvvmClean.Ioc;
using MvvmClean.View.Navigation;
=======
using CommonServiceLocator;
>>>>>>> 5cd7175206f4b8e2bdcd58b5950f6747d06a8593:View/Navigation/Presentation/FrameNavigationService.cs
using MvvmClean.ViewModel.Navigation;

namespace MvvmClean.Windows
{
    /// <summary>
    /// Service de navigation par Frame pour PresentationFramework
    /// Auteur: Nicolas Gidon)
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
<<<<<<< HEAD:Windows/FrameNavigationService.cs
        private readonly ContentControl _contentControl;
=======
        private Frame MainFrame => Application.Current.MainWindow?.Content as Frame;
>>>>>>> 5cd7175206f4b8e2bdcd58b5950f6747d06a8593:View/Navigation/Presentation/FrameNavigationService.cs

        /// <summary>
        /// Permet de dispatcher sur le thread UI
        /// </summary>
        private Dispatcher UiDispatcher => Application.Current.Dispatcher;

        /// <summary>
<<<<<<< HEAD:Windows/FrameNavigationService.cs
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
=======
        /// Configuration de la navigation
        /// </summary>
        /// <typeparam name="TContext">Le contexte de donnée a appliquer à la page</typeparam>
        /// <typeparam name="TPage">La page a associer</typeparam>
        /// <param name="key">La clef associée</param>
        public void Configure<TContext,TPage>(string key)where TPage:Page
        {
            _pages.Add(key, typeof(TPage));
            _contexts.Add(key, typeof(TContext));
        }

        #region Interface Implementation

        public bool CanGoBack => _navStack.Count != 0;

        public void GoBack()
        {
            GoBack(null);
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

>>>>>>> 5cd7175206f4b8e2bdcd58b5950f6747d06a8593:View/Navigation/Presentation/FrameNavigationService.cs
        public void GoBack(object parameter)
        {
            if (_navStack.Count > 0)
            {
                Page oldPage = _navStack.Pop();
<<<<<<< HEAD:Windows/FrameNavigationService.cs
                _contentControl.Content = oldPage;
=======
                if (MainFrame == null)
                    throw new Exception("Impossible de localiser la Frame de la fenêtre principale");
                MainFrame.Content = oldPage;
>>>>>>> 5cd7175206f4b8e2bdcd58b5950f6747d06a8593:View/Navigation/Presentation/FrameNavigationService.cs

                // envoi du paramètre si le DataContext implémente INavigable
                var navigable = oldPage.Content as INavigable;
                navigable?.OnBackedHere(parameter);
            }
        }

<<<<<<< HEAD:Windows/FrameNavigationService.cs
        /// <summary>
        /// Navigation vers la page associée à la clef avec passage de paramètre si le ViewModel correspondant
        /// implémente l'interface INavigable
        /// </summary>
        /// <param name="key">clef paramètrée pour la page</param>
        /// <param name="parameter">paramètre à transmettre au ViewModel s'il implémente INavigable</param>
        public virtual void NavigateTo(object key, object parameter)
=======
        public virtual void NavigateTo(string pageKey, object parameter)
>>>>>>> 5cd7175206f4b8e2bdcd58b5950f6747d06a8593:View/Navigation/Presentation/FrameNavigationService.cs
        {
            if (!_pages.ContainsKey(key))
            {
                throw new ArgumentException($"Page introuvable: {key} ", nameof(key));
            }
            lock (_pages)
            {
                // historisation de l'ancienne page
<<<<<<< HEAD:Windows/FrameNavigationService.cs
                var oldPage = _contentControl.Content as Page;
=======
                var oldPage = MainFrame.Content as Page;
>>>>>>> 5cd7175206f4b8e2bdcd58b5950f6747d06a8593:View/Navigation/Presentation/FrameNavigationService.cs
                if (oldPage != null)
                {
                    _navStack.Push(oldPage);
                }

                // Affection de la vue
<<<<<<< HEAD:Windows/FrameNavigationService.cs
                var page = (Page)Activator.CreateInstance(_pages[key]);
                _contentControl.Content = page; //n'affecte pas directement le Content (chargement interne a l'UI async)
=======
                var page = (Page)Activator.CreateInstance(_pages[pageKey]);
                if (MainFrame == null)
                    throw new Exception("Impossible de localiser la Frame de la fenêtre principale");
                MainFrame.Content = page; //n'affecte pas directement le Content (chargement interne a l'UI async)
>>>>>>> 5cd7175206f4b8e2bdcd58b5950f6747d06a8593:View/Navigation/Presentation/FrameNavigationService.cs

                object viewModel;
                // Chargement du ViewModel dans une task
                Task.Run(() =>
                {
                    // Passage de paramètre si le ViewModel implémente INavigable
<<<<<<< HEAD:Windows/FrameNavigationService.cs
                    viewModel = Locator.Current.Resolve(_contexts[key]);
=======
                    viewModel = ServiceLocator.Current.GetInstance(_contexts[pageKey]);
                    UiDispatcher.Invoke(() => page.DataContext = viewModel);
>>>>>>> 5cd7175206f4b8e2bdcd58b5950f6747d06a8593:View/Navigation/Presentation/FrameNavigationService.cs
                    var nav = viewModel as INavigable;
                    nav?.OnNavigatedHere(parameter);
                });
            }
        }

<<<<<<< HEAD:Windows/FrameNavigationService.cs
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
=======
        #endregion
>>>>>>> 5cd7175206f4b8e2bdcd58b5950f6747d06a8593:View/Navigation/Presentation/FrameNavigationService.cs
    }
}

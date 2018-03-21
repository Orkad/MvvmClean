using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CommonServiceLocator;
using MvvmClean.ViewModel.Navigation;

namespace MvvmClean.View.Navigation.Presentation
{
    /// <summary>
    /// Service de navigation par Frame pour PresentationFramework
    /// Auteur: Nicolas Gidon)
    /// </summary>
    public class FrameNavigationService : IStackNavigationService
    {
        /// <summary>
        /// Association d'une clef string avec un Type de Page
        /// </summary>
        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();

        /// <summary>
        /// Association d'une clef string avec un type de contexte (ViewModel)
        /// </summary>
        private readonly Dictionary<string, Type> _contexts = new Dictionary<string, Type>();

        /// <summary>
        /// Historique de navigation
        /// </summary>
        private readonly Stack<Page> _navStack = new Stack<Page>();

        /// <summary>
        /// Frame sur laquelle sera basé le service de navigation
        /// </summary>
        private Frame MainFrame => Application.Current.MainWindow?.Content as Frame;

        /// <summary>
        /// Permet de dispatcher sur le thread UI
        /// </summary>
        private Dispatcher UiDispatcher => Application.Current.Dispatcher;

        /// <summary>
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

        public void GoBack(object parameter)
        {
            if (_navStack.Count > 0)
            {
                Page oldPage = _navStack.Pop();
                if (MainFrame == null)
                    throw new Exception("Impossible de localiser la Frame de la fenêtre principale");
                MainFrame.Content = oldPage;

                // envoi du paramètre si le DataContext implémente INavigable
                var navigable = oldPage.Content as INavigable;
                navigable?.OnBackedHere(parameter);
            }
        }

        public virtual void NavigateTo(string pageKey, object parameter)
        {
            if (!_pages.ContainsKey(pageKey))
            {
                throw new ArgumentException($"Page introuvable: {pageKey} ", nameof(pageKey));
            }
            lock (_pages)
            {
                // historisation de l'ancienne page
                var oldPage = MainFrame.Content as Page;
                if (oldPage != null)
                {
                    _navStack.Push(oldPage);
                }

                // Affection de la vue
                var page = (Page)Activator.CreateInstance(_pages[pageKey]);
                if (MainFrame == null)
                    throw new Exception("Impossible de localiser la Frame de la fenêtre principale");
                MainFrame.Content = page; //n'affecte pas directement le Content (chargement interne a l'UI async)

                object viewModel;
                // Chargement du ViewModel dans une task
                Task.Run(() =>
                {
                    // Passage de paramètre si le ViewModel implémente INavigable
                    viewModel = ServiceLocator.Current.GetInstance(_contexts[pageKey]);
                    UiDispatcher.Invoke(() => page.DataContext = viewModel);
                    var nav = viewModel as INavigable;
                    nav?.OnNavigatedHere(parameter);
                });
            }
        }

        #endregion
    }
}

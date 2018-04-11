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
    /// Service de navigation par ViewModel
    /// </summary>
    public class ViewModelNavigationService : INavigationService
    {        
        /// <summary>
        /// Association ViewModel / View
        /// </summary>
        private readonly Dictionary<Type, Type> _configuration = new Dictionary<Type, Type>();
        private readonly Stack<Page> _navStack = new Stack<Page>();
        private readonly ContentControl _contentControl;
        private readonly Dispatcher _uiDispatcher;

        /// <summary>
        /// Créé une instance de service de navigation pour la frame passé en paramètre
        /// A appeller depuis le thread UI uniquement
        /// </summary>
        /// <param name="contentControl">Conteneur cible de la navigation</param>
        public ViewModelNavigationService(ContentControl contentControl)
        {
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
        /// <param name="viewModel">instance du ViewModel sur lequel naviguer</param>
        /// <param name="parameter">paramètre à transmettre au ViewModel</param>
        public void NavigateTo(object viewModel, object parameter)
        {
            if (!_configuration.ContainsKey(viewModel.GetType()))
            {
                throw new ArgumentException($"Page introuvable pour le type: {viewModel.GetType()} ", nameof(viewModel));
            }
            var obj = new object();
            lock (obj)
            {
                // historisation de l'ancienne page
                var oldPage = _contentControl.Content as Page;
                if (oldPage != null)
                {
                    _navStack.Push(oldPage);
                }

                // Affection de la vue
                var page = (Page)Activator.CreateInstance(_configuration[viewModel.GetType()]);
                _contentControl.Content = page; //n'affecte pas directement le Content (chargement interne a l'UI async)
                
                Task.Run(() =>
                {
                    // Passage de paramètre si le ViewModel implémente INavigable
                    var navigable = viewModel as INavigable;
                    navigable?.OnNavigatedHere(parameter);
                    _uiDispatcher.Invoke(() => page.DataContext = viewModel);
                });
            }
        }

        /// <summary>
        /// Configuration de la navigation
        /// </summary>
        /// <typeparam name="TContext">Le contexte de donnée associé à la page</typeparam>
        /// <typeparam name="TPage">La page associée</typeparam>
        public void Configure<TContext, TPage>() where TPage : Page
        {
            _configuration.Add(typeof(TContext), typeof(TPage));
        }
    }
}

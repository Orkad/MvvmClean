using System;
using System.Windows.Input;
using CommonServiceLocator;
using MvvmClean.Command;
using MvvmClean.View.Dialog;
using MvvmClean.View.Navigation;

namespace MvvmClean.ViewModel.Navigation
{
    public abstract class NavigableViewModelBase:ViewModelBase,INavigable
    {
        private readonly IStackNavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public ICommand BackCommand { get; }

        public ICommand GoToCommand { get; }

        protected NavigableViewModelBase()
        {
            if(!ServiceLocator.IsLocationProviderSet)
                throw new Exception("Le service locator doit être initialisé");
            BackCommand = new ActionCommand((s) => Back());
            GoToCommand = new ActionCommand<string>(s => NavigateTo(s));

            // permet de s'affranchir du passage du service de navigation dans le constructeur
            _navigationService = ServiceLocator.Current.GetInstance<IStackNavigationService>();
            _dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
        }

        protected void Back()
        {
            _navigationService.GoBack();
        }

        protected void NavigateTo(string pageKey, object parameter = null)
        {
            _navigationService.NavigateTo(pageKey, parameter);
        }

        protected void Alert(string message, string title = "Information")
        {
            _dialogService.Alert(message, title);
        }

        protected void Alert(Exception exception, string title = "Erreur")
        {
            _dialogService.Alert(exception.Message, title);
        }

        protected bool Confirm(string message, string title, string confirmText, string cancelText)
        {
            return _dialogService.AskConfirm(message, title, confirmText, cancelText).Result;
        }

        public virtual void OnNavigatedHere(object parameter = null)
        {

        }

        public virtual void OnBackedHere(object parameter = null)
        {

        }
    }
}

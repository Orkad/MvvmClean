using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MvvmClean.View.Dialog.Presentation
{
    public class WindowDialogService:IDialogService
    {
        private readonly Window _window;
        private readonly Dispatcher _uiDispatcher;

        public WindowDialogService(Window window)
        {
            _window = window;
            _uiDispatcher = Dispatcher.CurrentDispatcher;
        }

        public async Task Alert(string message, string title = "Information")
        {
            await _uiDispatcher.InvokeAsync(() => MessageBox.Show(_window, message, title, MessageBoxButton.OK));
        }

        public async Task<bool> AskConfirm(string message, string title = "Confirmation", string buttonConfirmText = "Confirmer",
            string buttonCancelText = "Annuler")
        {
            var dialogResult = await _uiDispatcher.InvokeAsync(() => MessageBox.Show(_window, message, title, MessageBoxButton.OKCancel));
            return dialogResult == MessageBoxResult.OK;
        }
    }
}

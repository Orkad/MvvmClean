using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MvvmClean.View.Dialog;

namespace MvvmClean.Windows
{
    public class WindowDialogService:IDialogService
    {
        private Window Window => Application.Current.MainWindow;
        private Dispatcher UiDispatcher => Application.Current.Dispatcher;

        public async Task Alert(string message, string title = "Information")
        {
            await UiDispatcher.InvokeAsync(() => MessageBox.Show(Window, message, title, MessageBoxButton.OK));
        }

        public async Task<bool> AskConfirm(string message, string title = "Confirmation", string buttonConfirmText = "Confirmer",
            string buttonCancelText = "Annuler")
        {
            var dialogResult = await UiDispatcher.InvokeAsync(() => MessageBox.Show(Window, message, title, MessageBoxButton.OKCancel));
            return dialogResult == MessageBoxResult.OK;
        }
    }
}

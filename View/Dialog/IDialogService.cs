using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MvvmClean.View.Dialog
{
    /// <summary>
    /// Service de dialogue avec l'utilisateur
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Notifie l'utilisateur
        /// </summary>
        /// <param name="message">Message à afficher a l'utilisateur</param>
        /// <param name="title">Titre du message à afficher ("Information" par défaut)</param>
        /// <returns></returns>
        Task Alert(string message, string title = "Information");

        /// <summary>
        /// Demande à l'utilisateur une confirmation
        /// </summary>
        /// <param name="message">Message à afficher à l'utilisateur</param>
        /// <param name="title">Titre du message à afficher</param>
        /// <param name="buttonConfirmText">Label du bouton permetant la confirmation ("Confirmer" par défaut)</param>
        /// <param name="buttonCancelText">Label du bouton permetant la non confirmation ("Annuler" par défaut)</param>
        /// <returns></returns>
        Task<bool> AskConfirm(string message, string title = "Confirmation", string buttonConfirmText = "Confirmer", string buttonCancelText = "Annuler");
    }
}

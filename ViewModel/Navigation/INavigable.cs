namespace MvvmClean.ViewModel.Navigation
{
    /// <summary>
    /// Defini quelque chose comme étant navigable
    /// à implémenter sur les ViewModel bindé sur les différentes page navigable de l'application
    /// </summary>
    public interface INavigable
    {
        /// <summary>
        /// Déclenché par le service de navigation lorsque l'on navigue vers ce ViewModel
        /// </summary>
        /// <param name="parameter">paramètre fourni</param>
        void OnNavigatedHere(object parameter = null);

        /// <summary>
        /// Déclenché par le service de navigation lorsqu'un retour vers ce ViewModel se produit
        /// </summary>
        /// <param name="parameter">paramètre fourni</param>
        void OnBackedHere(object parameter = null);
    }
}
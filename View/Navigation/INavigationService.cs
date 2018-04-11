namespace MvvmClean.View.Navigation
{
    /// <summary>
    /// Provide stacked navigation based on string keys
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Retourne vers la page précédente en lui transmettant un paramètre
        /// </summary>
        /// <param name="parameter">paramètre a transmetre a la page</param>
        void GoBack(object parameter = null);

        /// <summary>
        /// Navigue vers la page en lui transmettant un paramètre
        /// </summary>
        /// <param name="key">clef identifiant la page sur laquelle naviguer</param>
        /// <param name="parameter">paramètre a transmetre à la page</param>
        void NavigateTo(object key, object parameter = null);

    }
}
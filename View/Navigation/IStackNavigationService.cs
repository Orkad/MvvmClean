namespace MvvmClean.View.Navigation
{
    /// <summary>
    /// Provide stacked navigation based on string keys
    /// </summary>
    public interface IStackNavigationService
    {
        /// <summary>
        /// Retourne vers la page précédente
        /// </summary>
        void GoBack();

        /// <summary>
        /// Retourne vers la page précédente en lui transmettant un paramètre
        /// </summary>
        /// <param name="parameter">paramètre a transmetre a la page</param>
        void GoBack(object parameter);

        /// <summary>
        /// Navigue vers la page
        /// </summary>
        /// <param name="key">clef identifiant la page sur laquelle naviguer</param>
        void NavigateTo(string key);



        /// <summary>
        /// Navigue vers la page en lui transmettant un paramètre
        /// </summary>
        /// <param name="key">clef identifiant la page sur laquelle naviguer</param>
        /// <param name="parameter">paramètre a transmetre à la page</param>
        void NavigateTo(string key, object parameter);

    }
}
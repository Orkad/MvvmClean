namespace MvvmClean.View.Navigation
{
    /// <summary>
    /// Provide stacked navigation based on string keys
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
<<<<<<< HEAD:View/Navigation/INavigationService.cs
        /// Retourne vers la page précédente en lui transmettant un paramètre
        /// </summary>
        /// <param name="parameter">paramètre a transmetre a la page</param>
        void GoBack(object parameter = null);
=======
        /// Détermine si il est possible de revenir vers la page précédente
        /// </summary>
        bool CanGoBack { get; }

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
>>>>>>> 5cd7175206f4b8e2bdcd58b5950f6747d06a8593:View/Navigation/IStackNavigationService.cs

        /// <summary>
        /// Navigue vers la page en lui transmettant un paramètre
        /// </summary>
        /// <param name="key">clef identifiant la page sur laquelle naviguer</param>
        /// <param name="parameter">paramètre a transmetre à la page</param>
        void NavigateTo(object key, object parameter = null);

    }
}
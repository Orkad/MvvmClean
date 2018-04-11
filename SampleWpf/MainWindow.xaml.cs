using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MvvmClean.Ioc;
using MvvmClean.Ioc.Unity;
using MvvmClean.View.Dialog;
using MvvmClean.View.Navigation;
using MvvmClean.ViewModel.Navigation;
using MvvmClean.Windows;
using SampleWpf.Models;
using SampleWpf.Pages;
using SampleWpf.Services;
using SampleWpf.ViewModels;
using NavigationService = MvvmClean.Windows.NavigationService;

namespace SampleWpf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConfigureContainer();

            var mainViewModel = Locator.Current.Resolve<MainViewModel>();
            Locator.Current.Resolve<INavigationService>().NavigateTo(mainViewModel);
        }

        public void ConfigureContainer()
        {
            Locator.SetLocator<UnityLocator>();

            var navigationService = new ViewModelNavigationService(Application.Current.MainWindow);
            navigationService.Configure<MainViewModel, MainPage>();
            navigationService.Configure<CategoryViewModel, CategoryPage>();
            navigationService.Configure<ItemViewModel, ItemPage>();

            var dialogService = new WindowDialogService(Application.Current.MainWindow);

            Locator.Current.Register<INavigationService>(navigationService);
            Locator.Current.Register<IDialogService>(dialogService);
            Locator.Current.Register<ICrudService<Category>, CategoryService>();
            Locator.Current.Register<ICrudService<Item>, ItemService>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmClean.Command;
using MvvmClean.Ioc;
using MvvmClean.View.Navigation;
using MvvmClean.ViewModel;
using MvvmClean.ViewModel.Navigation;
using SampleWpf.Models;
using SampleWpf.Services;

namespace SampleWpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ICrudService<Category> _categoryCrudService;
        private ObservableCollection<CategoryViewModel> _categories;
        private CategoryViewModel _selectedCategory;

        public ObservableCollection<CategoryViewModel> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        public ICommand AddCategoryCommand { get; }

        public CategoryViewModel SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public ICommand NavigateToCategoryCommand { get; }

        public MainViewModel(INavigationService navigationService,
            ICrudService<Category> categoryCrudService)
        {
            _navigationService = navigationService;
            _categoryCrudService = categoryCrudService;
            _categories = new ObservableCollection<CategoryViewModel>(_categoryCrudService.Read().Select(c =>
            {
                var cat = Locator.Current.Resolve<CategoryViewModel>();
                cat.Init(c);
                return cat;
            }));

            AddCategoryCommand = new RelayCommand(AddCategory);
            NavigateToCategoryCommand = new RelayCommand((o) => _navigationService.NavigateTo(SelectedCategory), (o) => SelectedCategory != null);
        }

        private void AddCategory()
        {
            var newCategoryViewModel = Locator.Current.Resolve<CategoryViewModel>();
            newCategoryViewModel.Name = "Nouvelle Categorie";
            Categories.Add(newCategoryViewModel);
        }
    }
}

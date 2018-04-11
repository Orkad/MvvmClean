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
    public class CategoryViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ICrudService<Category> _categoryCrudService;
        private readonly ICrudService<Item> _itemCrudService;
        private string _name;
        private ObservableCollection<ItemViewModel> _items;
        private ItemViewModel _selectedItem;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ObservableCollection<ItemViewModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public ICommand BackCommand { get; }

        public ICommand AddItemCommand { get; }

        public ItemViewModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public CategoryViewModel(INavigationService navigationService,
            ICrudService<Category> categoryCrudService,
            ICrudService<Item> itemCrudService)
        {
            _navigationService = navigationService;
            _categoryCrudService = categoryCrudService;
            _itemCrudService = itemCrudService;

            BackCommand = new RelayCommand(() => _navigationService.GoBack());
        }

        public void Init(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Items = new ObservableCollection<ItemViewModel>(category.Items.Select(i =>
            {
                var viewModel = Locator.Current.Resolve<ItemViewModel>();
                viewModel.Init(i);
                return viewModel;
            }));
        }
    }
}

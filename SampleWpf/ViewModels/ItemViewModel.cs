using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmClean.ViewModel;
using MvvmClean.ViewModel.Navigation;
using SampleWpf.Models;

namespace SampleWpf
{
    public class ItemViewModel:ViewModelBase
    {
        private string _name;

        public int Id { get; private set; }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public void Init(Item i)
        {
            Id = i.Id;
            Name = i.Name;
        }
    }
}

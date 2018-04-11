using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleWpf.Models;

namespace SampleWpf.Services
{
    public class CategoryService : ICrudService<Category>
    {
        public Dictionary<int, Category> Categories { get; }
        private int _autoIncrement;

        public CategoryService()
        {
            Categories = FakeData.Categories.ToDictionary(c => c.Id);
            _autoIncrement = Categories.Count;
        }


        public int Create(Category item)
        {
            item.Id = ++_autoIncrement;
            Categories.Add(item.Id, item);
            return item.Id;
        }

        public List<Category> Read()
        {
            return Categories.Values.ToList();
        }

        public Category Read(int key)
        {
            if (Categories.ContainsKey(key))
            {
                return Categories[key];
            }

            return null;
        }

        public void Update(Category item)
        {
            
        }

        public void Delete(int key)
        {
            Categories.Remove(key);
        }
    }
}

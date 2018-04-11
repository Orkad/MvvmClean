using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleWpf.Models;

namespace SampleWpf.Services
{
    public class ItemService : ICrudService<Item>
    {
        public List<Item> Items { get; }

        public ItemService()
        {
            Items = FakeData.Items.ToList();
            _autoIncrement = Items.Count;
        }

        private int _autoIncrement;

        public int Create(Item item)
        {
            item.Id = ++_autoIncrement;
            Items.Add(item);
            return item.Id;
        }

        public List<Item> Read()
        {
            return Items;
        }

        public Item Read(int key)
        {
            return Items.SingleOrDefault(i => i.Id == key);
        }

        public void Update(Item item)
        {
            
        }

        public void Delete(int key)
        {
            var index = Items.FindIndex(i => i.Id == key);
            Items.RemoveAt(index);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleWpf.Models;

namespace SampleWpf.Services
{
    public static class FakeData
    {
        public static IEnumerable<Category> Categories { get; }
        public static IEnumerable<Item> Items { get; }

        static FakeData()
        {
            Categories = new List<Category> {new Category
            {
                Id = 1, Name = "Fruits"
            }, new Category(){Id = 2, Name = "Legumes"}};

            Items = new List<Item>
            {
                new Item {Id = 1, Name = "Pomme", CategoryId = 1},
                new Item {Id = 2, Name = "Tomate", CategoryId = 1},
                new Item {Id = 3, Name = "Noix de Coco", CategoryId = 1},
                new Item {Id = 4, Name = "Cerise", CategoryId = 1},

                new Item {Id = 5, Name = "Pomme de terre", CategoryId = 2},
                new Item {Id = 6, Name = "Concombre", CategoryId = 2},
                new Item {Id = 7, Name = "Carotte", CategoryId = 2},
            };

            foreach (var c in Categories)
            {
                c.Items = new List<Item>();
            }

            foreach (var i in Items)
            {
                var cat = Categories.SingleOrDefault(c => i.CategoryId == c.Id);
                if (cat != null)
                {
                    cat.Items.Add(i);
                    i.Category = cat;
                }
            }
        }
    }
}

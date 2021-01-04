using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteAndGo.Models;

namespace VoteAndGo.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Step 1", Description="Click the Take Picture tab from the side munu." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Step 2", Description="Make sure to take a clear picture of your ID card." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Step 3", Description="The app extracts your personal information and places it in a list." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Step 4", Description="Click the My Information tab from the side menu to view your data." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Step 5", Description="Click the Save as PDF tab from the side menu to save the voting declaration to your phone." },
            };
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
using Collect.io.DAL.Models;

namespace Collect.io.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel(List<int> collectionIds, List<int> frequency,List<ItemModel> items, List<string> tags, List<CollectionModel> collections)
        {
            this.CollectionIds = collectionIds;
            this.CollectionFrequency = frequency;
            this.LatestItems = items;
            this.Tags = tags;
            this.Collections = collections;


        }
        public List<ItemModel>? LatestItems { get; set; }
        
        public List<int>? CollectionIds { get; set;}
        public List<CollectionModel>? Collections { get; set; }
        public List<int>? CollectionFrequency { get; set; }
        
        public List<string>? Tags { get; set; }
    }
}

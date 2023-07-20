using System.ComponentModel.DataAnnotations;

namespace Collect.io.DAL.Models
{
    public class CollectionModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Topic Topic { get; set; }
        public string? ImageUrl { get; set; }
        public List<ItemModel>? Items { get; set; }
        //one-to-many with the user - how will admins work?
        public int? UserId { get; set; }
        public UserModel? User { get; set; }

        //one-to-many - collection->fields
        public List<FieldConfModel>? Fields { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Collect.io.DAL.Models
{
    public class UserModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public List<CollectionModel>? Collections { get; set; }
    }
}

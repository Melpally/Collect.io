using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Collect.io.DAL.Models
{
    public class FieldConfModel
    {
        [Key]
        public int Id { get; set; }
        //one-to-many - collection -> fields
        public int CollectionId { get; set; }
        public CollectionModel? Collection { get; set; } 
        public string? Name { get; set; } 
        public string? FieldType { get; set; }

        //one to one relationship with the values table
        public FieldValuesModel? ValueId { get; set; }

    }
}

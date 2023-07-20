using System.ComponentModel.DataAnnotations;

namespace Collect.io.DAL.Models
{
    public class ItemModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime Date { get; set; }
        public string Tags { get; set; } = null!;
        public int CollectionId { get; set; } 
        public CollectionModel? Collection { get; set; }
        public FieldValuesModel? FieldValue { get; set; }

        /*public string? stringField1 { get; set; }
        public string? stringField2 { get; set; }
        public string? stringField3 { get; set; }
        public long? longField1 { get; set; }
        public long? longField2 { get; set; }
        public long? longField3 { get; set; }
        public bool? boolField1 { get; set; }
        public bool? boolField2 { get; set; }
        public bool? boolField3 { get; set; }
        public DateTime? dateTimeField1 { get; set; }
        public DateTime? dateTimeField2 { get; set; }
        public DateTime? dateTimeField3 { get; set; }
        public string? multilineString1 { get; set; }
        public string? multilineString2 { get; set; }
        public string? multilineString3 { get; set; }*/
    }
}

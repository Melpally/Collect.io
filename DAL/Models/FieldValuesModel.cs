using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Collect.io.DAL.Models
{
    public class FieldValuesModel
    {
        [Key]
        public int Id { get; set; }
        public int? ItemId { get; set; }
        public ItemModel? Item { get; set; }
        public int? TypeId { get; set; }
        public FieldConfModel? Type { get; set; }
        public string? Value { get; set; }
    }
}

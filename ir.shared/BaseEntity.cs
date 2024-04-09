using System.ComponentModel.DataAnnotations;

namespace ir.shared
{
    public abstract class BaseEntity
    {
        [Required, Key]
        public int Id { get; set; }
    }
}

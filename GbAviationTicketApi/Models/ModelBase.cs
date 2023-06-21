using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GbAviationTicketApi.Models
{
    public abstract class ModelBase
    {
        public ModelBase()
        {
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = null;
        }

        [Required]
        [Column("IS_ACTIVE")]
        public bool? IsActive { get; set; }

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }

        [Column("MODIFIED_AT")]
        public DateTime? ModifiedAt { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementAdmin.Domain.Entity
{
    public class Entity
    {
        public bool IsDelete { get; set; } = false;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateAt { get; set; }

        public DateTime DeleteAt { get; set; }
    }

    public class Entity<TId> : Entity
    {
        [Key]
        public TId Id { get; set; }
    }
}

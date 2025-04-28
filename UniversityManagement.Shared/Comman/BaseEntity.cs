using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagement.Shared.Comman
{
    public abstract class BaseEntity<TKey> : IEntityBase<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; } = default!;
    }
    public abstract class DeleteEntity<TKey> : BaseEntity<TKey>, IDeleteEntity<TKey>
    {
        public bool IsDeleted { get; set; }
    }

    public abstract class AuditEntity<TKey> : DeleteEntity<TKey>, IEntityBase<TKey>
    {
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }

        public void SetCreator(string creator, DateTime dateTime)
        {
            this.CreatedBy = !string.IsNullOrWhiteSpace(creator) ? Guid.Parse(creator) : null;
            this.CreatedDate = dateTime;
        }

        public void SetUpdater(string updater, DateTime dateTime)
        {
            this.UpdatedBy = !string.IsNullOrWhiteSpace(updater) ? Guid.Parse(updater) : null;
            this.UpdatedDate = dateTime;
        }
    }
}

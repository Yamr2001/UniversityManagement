namespace UniversityManagement.Shared.Comman
{
    public interface IEntityBase<TKey>
    {
        TKey Id { get; set; }
    }
    public interface IDeleteEntity
    {
        bool IsDeleted { get; set; }
    }
    public interface IDeleteEntity<TKey> : IDeleteEntity, IEntityBase<TKey>
    {
    }
    public interface IEntityBase
    {
        DateTime CreatedDate { get; set; }
        Guid? CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        Guid? UpdatedBy { get; set; }
        void SetCreator(string creator, DateTime dateTime);

        void SetUpdater(string updater, DateTime dateTime);

    }
    public interface IBaseEntity<TKey> : IEntityBase, IDeleteEntity<TKey>
    {
    }
}

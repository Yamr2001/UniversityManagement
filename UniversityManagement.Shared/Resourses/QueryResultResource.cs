namespace UniversityManagement.Shared.Resourses
{
    public class QueryResultResource<TEntity>
    {
        public int TotalItems { get; set; }
        public IEnumerable<TEntity> Items { get; set; } = [];
    }
}

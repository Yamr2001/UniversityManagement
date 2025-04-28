namespace UniversityManagement.Shared.Resourses
{
    public class QueryResult<TEntity>
    {
        public int TotalItems { get; set; }
        public IEnumerable<TEntity> Items { get; set; } = [];
    }
}

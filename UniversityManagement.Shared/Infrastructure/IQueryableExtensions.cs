using System.Linq.Expressions;
using UniversityManagement.Shared.Helpers;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Shared.Infrastructure
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
                return query;

            if (queryObj.IsSortAscending)
                return query.OrderBy(columnsMap[queryObj.SortBy]);
            else
                return query.OrderByDescending(columnsMap[queryObj.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj)
        {
            if (queryObj.PageSize <= 0)
                queryObj.PageSize = 40;

            if (queryObj.Page <= 0)
                queryObj.Page = 1;

            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }

        public static IQueryable<T> ApplyFiltering<T, M>(this IQueryable<T> query, M filter, Dictionary<string, Expression<Func<T, bool>>> columns, List<Expression<Func<T, bool>>> orConditions)
        {
            var filteredQuery = query; // Initialize a filtered query variable

            foreach (var column in columns)
            {
                var expression = column.Value;
                var hasValue = column.Key;

                if (hasValue.Contains("True"))
                {
                    filteredQuery = filteredQuery.Where(expression); // Apply each Where clause
                }
            }
            if (orConditions.Any())
            {
                var orExpression = orConditions.Aggregate((current, next) => current.Or(next));
                filteredQuery = filteredQuery.Where(orExpression);
            }
            return filteredQuery;
        }


        public static IEnumerable<T> IApplyOrdering<T>(this IEnumerable<T> list, IQueryObject queryObj, Dictionary<string, Func<T, object>> columnsMap)
        {
            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
                return list;

            if (queryObj.IsSortAscending)
                return list.OrderBy(columnsMap[queryObj.SortBy]);
            else
                return list.OrderByDescending(columnsMap[queryObj.SortBy]);
        }

        public static IEnumerable<T> IApplyPaging<T>(this IEnumerable<T> list, IQueryObject queryObj)
        {
            if (queryObj.PageSize <= 0)
                queryObj.PageSize = 10;

            if (queryObj.Page <= 0)
                queryObj.Page = 1;

            return list.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }

        public static IEnumerable<T> IApplyFiltering<T, M>(this IEnumerable<T> list, M filter, Dictionary<string, Func<T, bool>> columns)
        {
            var filteredList = list; // Initialize a filtered list variable

            foreach (var column in columns)
            {
                var predicate = column.Value;
                var hasValue = column.Key;

                if (hasValue.Contains("True"))
                {
                    filteredList = filteredList.Where(predicate); // Apply each Where clause
                }
            }

            return filteredList;
        }
    }
}

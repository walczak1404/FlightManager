using Microsoft.EntityFrameworkCore;

namespace FlightManager.Core
{
    /// <summary>
    /// Custom list implementation to implement pagination
    /// </summary>
    /// <typeparam name="T">Type of items list</typeparam>
    public class PagedList<T>
    {
        public PagedList(List<T> items, int page, int totalPagesCount)
        {
            Items = items;
            Page = page;
            TotalPagesCount = totalPagesCount;
        }

        public List<T> Items { get; }

        public int Page { get; }

        public int TotalPagesCount { get; }

        public bool HasNextPage => Page * 10 < TotalPagesCount;

        public bool HasPreviousPage => Page > 1;

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page)
        {
            var totalPagesCount = await query.CountAsync() / 10;
            var items = await query.Skip((page - 1) * 10).Take(10).ToListAsync();

            return new(items, page, totalPagesCount);
        }
    }
}

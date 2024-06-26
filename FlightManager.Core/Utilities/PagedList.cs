﻿using Microsoft.EntityFrameworkCore;

namespace FlightManager.Core.Utilities
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

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page)
        {
            var totalItems = await query.CountAsync();
            var totalPagesCount = (int)Math.Ceiling((double)totalItems / 10);
            var items = await query.Skip((page - 1) * 10).Take(10).ToListAsync();

            return new(items, page, totalPagesCount);
        }
    }
}

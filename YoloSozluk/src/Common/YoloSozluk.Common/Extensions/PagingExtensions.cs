using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Common.Models.Pages;
using YoloSozluk.Common.Models.ViewModels;

namespace YoloSozluk.Common.Extensions
{
    public static class PagingExtensions
    {
        public static async Task<PagedViewModel<T>> GetPaged<T> (this IQueryable<T> query, int currentPage, int pageSize) where T: class
        {
            var count = await query.CountAsync();

            Page page = new(currentPage, pageSize, count);

            var data = await query.Skip(page.SkippedRows).Take(page.PageSize).AsNoTracking().ToListAsync();

            var result = new PagedViewModel<T>(data, page);

            return result;
        }
    }
}

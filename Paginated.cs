using Microsoft.EntityFrameworkCore;
using ProjectMaking.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Helper
{
    public class Paginated<T>:List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPage { get; private set; }

        public Paginated(List<T>items,int count,int pageIndex,int pageSize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool HasNextPage 
        {
            get
            {
                return (PageIndex > TotalPage);
            }
        }

        public static async Task<Paginated<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Paginated<T>(items, count, pageIndex, pageSize);
        }

      
    }
}

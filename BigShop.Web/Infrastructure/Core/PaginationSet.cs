using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigShop.Web.Infrastructure.Core
{
    public class PaginationSet<T>
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int MaxPage { get; set; }
        public int TotalCount { get; set; }

        public int Count
        {
            get
            {
                return (Items != null) ? Items.Count() : 0;
            }
        }

        public IEnumerable<T> Items { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.models
{
    public class PagedResult<T>
    {
        public List<T> Iteams { get; set; }
        public int TotalPages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItemsCount { get; set; }
       
        public PagedResult(List<T> iteams, int totalCount, int pageSize, int pageNumber)
        {
           
            Iteams = iteams;
            TotalItemsCount = totalCount;
            ItemFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemFrom + pageSize - 1;
            TotalPages = (int)Math.Round(totalCount /(double) pageSize,0);
        }

    }
}

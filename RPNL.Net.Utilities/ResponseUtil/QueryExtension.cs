using System.Collections.Generic;
using System.Linq;

namespace RPNL.Net.Utilities.ResponseUtil
{
    public static class QueryExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDataTable">Only set true when pagination parameters is coming from Jquery Datatable</param>
        /// <returns></returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageSize, bool isDataTable = false)
        {
            if (isDataTable)
            {
                var entities = query.Skip(pageIndex).Take(pageSize);
                return entities;
            }
            else
            {
                var entities = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return entities;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDataTable">Only set true when pagination parameters is coming from Jquery Datatable</param>
        /// <returns></returns>
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> query, int pageIndex, int pageSize, bool isDataTable = false)
        {
            if (isDataTable)
            {
                var entities = query.Skip(pageIndex).Take(pageSize);
                return entities;
            }
            else
            {
                var entities = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return entities;
            }
        }
    }
}

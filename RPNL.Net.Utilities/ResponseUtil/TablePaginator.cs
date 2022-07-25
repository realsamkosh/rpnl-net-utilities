using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNL.Net.Utilities.ResponseUtil
{
    public class TablePaginator
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Skip
        {
            get
            {
                if (Page <= 0 || Page == 1)
                {
                    return 0;
                }
                else if (Page > 1 && Page < 500)
                {
                    return (Page - 1) * PageSize;
                }
                else
                {
                    Page = 750;
                    return (Page - 1) * PageSize;
                }
            }
        }
    }
}

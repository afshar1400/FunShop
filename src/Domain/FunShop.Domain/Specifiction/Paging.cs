using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Domain.Specifiction
{
    public class Paging
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public int NumberOfPages { get; set; }
        public string Search { get; set; }

        public bool HasNext
        {
            get
            {
                if (PageIndex < NumberOfPages)
                {
                    return true;
                }
                return false;
            }
        }
        public bool HasPrev
        {
            get
            {
                if (PageIndex > 1)
                {
                    return true;
                }
                return false;
            }
        }


    }
}

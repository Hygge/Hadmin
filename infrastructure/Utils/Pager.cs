using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Utils
{
    public class Pager<T>
    {
        public int pageNum { set; get; }
        public int pageSize { set; get; }
        public long total { set; get; }
        public List<T> rows { set; get; }

        public Pager(int pageNum, int pageSize)
        {
            this.pageNum = pageNum;
            this.pageSize = pageSize;
        }



        public int getSkip()
        {
            return (pageNum - 1) * pageSize;
        }




    }
}

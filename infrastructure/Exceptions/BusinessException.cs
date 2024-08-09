using domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public int code { set; get; }
        public string message { set; get; }
        public BusinessException(string message)
        {
            this.code = HttpCode.FAILED_CODE;
            this.message = message;
        }
        public BusinessException(int code, string message)
        {
            this.code = code;
            this.message = message;
        }


    }
}

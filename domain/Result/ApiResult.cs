using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Result
{
    public class ApiResult
    {

        public int code { get; set; } = HttpCode.SUCCESS_CODE;
        public string message { get; set; } = "ok！";
        public object? data { get; set; }
        public string timestamp { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


        public ApiResult() { }
        public ApiResult(int code, string message, object? data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
        public ApiResult(int code, string message)
        {
            this.code = code;
            this.message = message;
        }
        public ApiResult(int code, object? data)
        {
            this.code = code;
            this.data = data;
        }

        public static ApiResult succeed(object? data = null)
        {
            return new ApiResult(HttpCode.SUCCESS_CODE, data);
        }
        public static ApiResult failed(int code, string message)
        {
            return new ApiResult(code, message);
        }


    }
}

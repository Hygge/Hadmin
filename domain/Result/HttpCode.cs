using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Result
{
    public class HttpCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        public readonly static int SUCCESS_CODE = 200;
        /// <summary>
        /// 失败
        /// </summary>
        public readonly static int FAILED_CODE = 424;
        /// <summary>
        /// 权限不足
        /// </summary>
        public readonly static int PERMISSIONS_CODE = 403;
        /// <summary>
        /// 未授权、未登录
        /// </summary>
        public readonly static int UNAUTHORIZED_CODE = 401;

    }
}

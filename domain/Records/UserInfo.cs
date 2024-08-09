using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Records
{
    public record UserInfo(long id, string userName, string nickName, string phone, string remark);

}

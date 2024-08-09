using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Enums
{
    public enum MenuEnum : Int32
    {

        [Description("目录")]
        Dir = 1,
        [Description("菜单")]
        Menu = 2,
        [Description("按钮")]
        Button = 3

    }
}

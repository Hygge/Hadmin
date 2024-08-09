using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Enums
{
    public enum DisabledEnum : Int32
    {
        [Description("启用")]
        ZERO = 0,
        [Description("禁用")]
        ONE = 1,
    }
}

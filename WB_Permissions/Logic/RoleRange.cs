using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB_Permissions
{
    public enum RoleRange
    {
        None = 0,
        User = 1,
        BusinessUnit = 2,
        ParentBusinessUnit = 4,
        Organization = 8
    }
}

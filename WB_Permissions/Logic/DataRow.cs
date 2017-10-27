using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB_Permissions
{
    public class DataRow
    {
        public string EntityName { get; set; }
        public string EntityLogicalName { get; set; }
        public string Role { get; set; }
        public RoleRange Read { get; set; }
        public RoleRange Write { get; set; }
        public RoleRange Delete { get; set; }
        public RoleRange Append { get; set; }
        public RoleRange AppendTo { get; set; }
        public RoleRange Assign { get; set; }
        public RoleRange Share { get; set; }
    }
}

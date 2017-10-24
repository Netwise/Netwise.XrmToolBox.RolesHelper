using System;

namespace Netwise.XrmToolBox.RolesHelper.Models
{
    /// <summary>
    /// SystemUser description.
    /// </summary>
    public class ModelSystemUser
    {
        public Guid SystemUserId { get; set; }
        public string FullName { get; set; }

        public class Fields
        {
            public const string SystemUserId = "systemuserid";
            public const string FullName = "fullname";
        }
    }
}
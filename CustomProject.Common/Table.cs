using System;

namespace CustomProject.Common
{
    public class Table:Attribute
    {
        public string TableName { get; set; }
        public string PrimaryColumn { get; set; }
        public string IdentityColumn { get; set; }
    }
}

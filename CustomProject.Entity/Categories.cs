using CustomProject.Common;

namespace CustomProject.Entity
{
    [Table(PrimaryColumn ="CategoryID", TableName ="Categories",IdentityColumn ="CategoryID")]
    public class Categories
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("BlogTag")]
    public class BlogTag: DomainEntity<int>
    {
        public int BlogId { get; set; }
        
        [Required] [StringLength(50), Column(TypeName = "varchar")]
        public int TagId { get; set; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }
        
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
        
    }
}

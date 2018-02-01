using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("BlogTag")]
    public class BlogTag : DomainEntity<int>
    {
        public int BlogId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string TagId { get; set; }

        [ForeignKey("BlogId")] public virtual Blog Blog { get; set; }

        [ForeignKey("TagId")] public virtual Tag Tag { get; set; }
    }
}
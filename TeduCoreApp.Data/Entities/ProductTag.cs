using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductTag")]
    public class ProductTag : DomainEntity<int>
    {
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string TagId { get; set; }

        [ForeignKey("ProductId")] public Product Product { get; set; }

        [ForeignKey("TagId")] public Tag Tag { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductImage")]
    public class ProductImage : DomainEntity<int>
    {
        public int ProductId { get; set; }

        [ForeignKey("ProductId")] public virtual Product Product { get; set; }

        [StringLength(256)] public string Path { get; set; }

        [StringLength(256)] public string Caption { get; set; }
    }
}
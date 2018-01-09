using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductQuantity")]
    public class ProductQuantity : DomainEntity<int>
    {
        [Column(Order = 1)] public int ProductId { get; set; }

        [Column(Order = 1)] public int SizeId { get; set; }
        [Column(Order = 1)] public int ColorId { get; set; }

        [ForeignKey("ProductId")] public virtual Product Product { get; set; }

        [ForeignKey("SizeId")] public virtual Size Size { get; set; }

        [ForeignKey("ColorId")] public virtual Color Color { get; set; }
    }
}
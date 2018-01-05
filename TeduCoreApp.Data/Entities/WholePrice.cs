using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("WholePrice")]
    public class WholePrice : DomainEntity<int>
    {
        public int ProductId { get; set; }
        public int FromQuantity { get; set; }
        public int ToQuantity { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("ProductId")] public virtual Product Product { get; set; }
    }
}
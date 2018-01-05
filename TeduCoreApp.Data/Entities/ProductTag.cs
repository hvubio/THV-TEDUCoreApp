using System.ComponentModel.DataAnnotations.Schema;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductTag")]
    public class ProductTag
    {
        public int ProductId { get; set; }
        public int TagId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }

    }
}

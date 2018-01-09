using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductTag")]
    public class ProductTag
    {
        public int ProductId { get; set; }
        [Required] [StringLength(50)]
        public string TagId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductImage")]
    public class ProductImage: DomainEntity<int>    
    {
        public int  ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public string Path { get; set; }
        public string Caption { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductColor")]
    public class ProductColor : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }

        [ForeignKey("ProductId")] public virtual Product Product { get; set; }

        [ForeignKey("ColorId")] public virtual Color Color { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public Status Status { get; set; }
    }
}
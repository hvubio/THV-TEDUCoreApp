using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Color")]
    public class Color : DomainEntity<int>, ISwitchable
    {
        [Required] [StringLength(256)] public string Name { get; set; }

        [Required] [StringLength(20)] public string Code { get; set; }

        public ICollection<ProductColor> ProductColors { get; set; }
        public Status Status { get; set; }
    }
}
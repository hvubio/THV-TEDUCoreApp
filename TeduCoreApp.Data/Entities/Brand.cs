using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Brand")]
    public class Brand : DomainEntity<int>, ISwitchable
    {
        [Required] [StringLength(256)] public string Name { get; set; }

        [Required] [StringLength(400)] public string Image { get; set; }

        public Status Status { get; set; }
    }
}
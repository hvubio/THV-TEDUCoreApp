using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Language")]
    public class Language : DomainEntity<int>, ISwitchable
    {
        [Required] [StringLength(128)] public string Name { get; set; }
        public bool IsDefault { get; set; }
        public string Resource { get; set; }

        public Status Status { get; set; }  
    }
}
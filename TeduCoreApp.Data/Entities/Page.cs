using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Page")]
    public class Page : DomainEntity<int>, ISwitchable
    {
        [Required] [StringLength(256)] public string Name { get; set; }

        [Required] [StringLength(256)] public string Alias { get; set; }

        public string Content { get; set; }

        public Status Status { get; set; }
    }
}
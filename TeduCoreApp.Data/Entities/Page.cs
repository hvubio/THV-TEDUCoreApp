using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Page")]
    public class Page: DomainEntity<int>, ISwitchable
    {
        public string Name { get; set; }
        public string Alias { get; set; }

        public string Content { get; set; }

        public Status Status { get; set; }
    }
}

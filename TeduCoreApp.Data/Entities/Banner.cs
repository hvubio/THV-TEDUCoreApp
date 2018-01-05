using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Banner")]
    public class Banner: DomainEntity<int>, ISwitchable
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public Status Status { get; set; }
    }
}

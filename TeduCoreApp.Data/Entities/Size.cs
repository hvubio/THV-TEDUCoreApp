using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Size")]
    public class Size : DomainEntity<int>
    {
        [StringLength(250)] public string Name { get; set; }
    }
}
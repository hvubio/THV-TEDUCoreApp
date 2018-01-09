using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Footer")]
    public class Footer : DomainEntity<int>
    {
        [Required] public string Content { get; set; }
    }
}
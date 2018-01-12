using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Footer")]
    public class Footer : DomainEntity<string>
    {
        [Required] public string Content { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Menu")]
    public class Menu : DomainEntity<int>, ISortable, ISwitchable
    {
        [Required] [StringLength(128)]
        public string Name { get; set; }
        [StringLength(256)]
        public string Url { get; set; }
        public string Css { get; set; }
        public int ParentId { get; set; }

        public int SortOrder { get; set; }
        public Status Status { get; set; }
    }
}
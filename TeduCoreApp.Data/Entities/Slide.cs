using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Slide")]
    public class Slide : DomainEntity<int>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }

        [Required] [StringLength(250)] public string GroupAlias { get; set; }

        public int DisplayOrder { get; set; }

        public bool Status { get; set; }
    }
}
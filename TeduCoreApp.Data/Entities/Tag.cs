using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Tag")]
    public class Tag : DomainEntity<int>
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public ICollection<BlogTag> BlogTags { get; set; }
    }
}